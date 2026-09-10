using System.Text;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using System.Security.Cryptography;
using Library.Domain.Interfaces.Auth;
using Library.Domain.Services.Auth;

namespace Library.Infrastructure.Services {
    public class RecuperacaoSenhaService : IRecuperacaoSenhaService {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IRecuperacaoSenhaRepository _passwordResetRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public RecuperacaoSenhaService(IUsuariosRepository usuariosRepository, IRecuperacaoSenhaRepository passwordResetRepository, IPasswordHasher passwordHasher, IEmailService emailService, IConfiguration configuration) {
            _usuariosRepository = usuariosRepository;
            _passwordResetRepository = passwordResetRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task SolicitarRecuperacaoAsync(string email) {
            if (string.IsNullOrWhiteSpace(email)) {
                throw new ArgumentException("O e-mail é obrigatório.");
            }

            email = email.Trim();

            var usuario = await _usuariosRepository.ObterPorEmailAsync(email);

            if (usuario is null) return;

            if (!usuario.X_ATIVO) return;

            await _passwordResetRepository.InvalidarTokensAnterioresAsync(usuario.CD_USUARIO);

            var token = GerarToken();

            var tokenHash = GerarHashToken(token);

            var recuperacao = new RecuperacaoSenha {
                CD_USUARIO = usuario.CD_USUARIO,
                DS_TOKEN = tokenHash,
                DT_EXPIRACAO = DateTime.Now.AddMinutes(30),
                DT_CADASTRO = DateTime.Now
            };

            await _passwordResetRepository.CriarAsync(recuperacao);

            var frontendUrl = _configuration["Frontend:Url"];

            if (string.IsNullOrWhiteSpace(frontendUrl)) {
                throw new InvalidOperationException("A URL do frontend não foi configurada.");
            }

            var link = $"{frontendUrl}/reset-password?token={token}";

            var assunto = "Redefinição de senha - Library";

            var corpo = $@"
                <!DOCTYPE html>
                <html lang=""pt-BR"">
                    <head>
                        <meta charset=""UTF-8"">
                        <title>Redefinição de Senha</title>
                    </head>
                    <body style=""margin:0; padding:0; background-color:#eef3f8; font-family: Arial, Helvetica, sans-serif;"">
                        <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#eef3f8; padding:30px 0;"">
                            <tr>
                                <td align=""center"">
                                    <table role=""presentation"" width=""480"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 2px 8px rgba(0,0,0,0.08);"">

                                        <!-- Cabeçalho -->
                                        <tr>
                                            <td align=""center"" style=""background-color:#0b3d66; padding:28px 20px;"">
                                                <h1 style=""margin:0; color:#ffffff; font-size:20px; font-weight:600;"">
                                                    Redefinição de Senha
                                                </h1>
                                            </td>
                                        </tr>

                                        <!-- Corpo -->
                                        <tr>
                                            <td style=""padding:32px 36px; color:#1f2d3d;"">
                                                <p style=""margin:0 0 16px; font-size:15px; line-height:1.6;"">
                                                    Olá, <strong>{usuario.DS_USUARIO}</strong>.
                                                </p>

                                                <p style=""margin:0 0 16px; font-size:15px; line-height:1.6;"">
                                                    Recebemos uma solicitação para redefinir a senha da sua conta.
                                                    Para continuar, clique no botão abaixo e crie uma nova senha:
                                                </p>

                                                <table role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""margin:24px auto;"">
                                                    <tr>
                                                        <td align=""center"" bgcolor=""#1565a8"" style=""border-radius:6px;"">
                                                            <a href=""{link}""
                                                                style=""display:inline-block; padding:13px 28px; font-size:15px; font-weight:600; color:#ffffff; text-decoration:none; border-radius:6px; background-color:#1565a8;"">
                                                                Redefinir minha senha
                                                            </a>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <p style=""margin:0 0 16px; font-size:14px; line-height:1.6; color:#4a5b6c;"">
                                                    Este link é válido por <strong>30 minutos</strong>. Após esse período, será necessário solicitar uma nova redefinição.
                                                </p>

                                                <p style=""margin:0; font-size:14px; line-height:1.6; color:#4a5b6c;"">
                                                    Se você não solicitou essa alteração, pode ignorar este e-mail com segurança. Sua senha permanecerá inalterada.
                                                </p>
                                            </td>
                                        </tr>

                                        <!-- Rodapé -->
                                        <tr>
                                            <td align=""center"" style=""background-color:#eaf1f8; padding:18px 20px;"">
                                                <p style=""margin:0; font-size:12px; color:#6b7f92;"">
                                                    Este é um e-mail automático, por favor não responda.
                                                </p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </body>
                </html>
            ";

            await _emailService.EnviarAsync(usuario.DS_EMAIL, assunto, corpo);
        }

        public async Task<bool> ValidarTokenAsync(string token) {
            if (string.IsNullOrWhiteSpace(token)) {
                return false;
            }

            var tokenHash = GerarHashToken(token);

            var recuperacao = await _passwordResetRepository.ObterPorTokenAsync(tokenHash);

            if (recuperacao is null) return false;

            if (recuperacao.DT_UTILIZACAO.HasValue) return false;

            if (recuperacao.DT_EXPIRACAO <= DateTime.Now) return false;

            return true;
        }

        public async Task<bool> RedefinirSenhaAsync(string token, string novaSenha) {
            if (string.IsNullOrWhiteSpace(token)) {
                return false;
            }

            if (string.IsNullOrWhiteSpace(novaSenha)) {
                throw new ArgumentException("A nova senha é obrigatória.");
            }

            if (novaSenha.Length < 8) {
                throw new ArgumentException( "A nova senha deve possuir pelo menos 8 caracteres.");
            }

            var tokenHash = GerarHashToken(token);

            var recuperacao = await _passwordResetRepository.ObterPorTokenAsync(tokenHash);

            if (recuperacao is null) {
                return false;
            }

            if (recuperacao.DT_UTILIZACAO.HasValue) {
                return false;
            }

            if (recuperacao.DT_EXPIRACAO <= DateTime.Now) {
                return false;
            }

            var usuario = await _usuariosRepository.ObterPorIdAsync(recuperacao.CD_USUARIO);

            if (usuario is null) {
                return false;
            }

            usuario.DS_SENHA = _passwordHasher.Hash(novaSenha);
            usuario.DT_ATUALIZACAO = DateTime.Now;

            var atualizado = await _usuariosRepository.AtualizarAsync(usuario);

            if (!atualizado) {
                return false;
            }

            await _passwordResetRepository.MarcarComoUtilizadoAsync(recuperacao.CD_RECUPERACAO);

            return true;
        }

        private static string GerarToken() {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToHexString(bytes);
        }

        private static string GerarHashToken(string token) {
            var bytes = Encoding.UTF8.GetBytes(token);

            var hash = SHA256.HashData(bytes);

            return Convert.ToHexString(hash);
        }
    }
}