using Library.Domain.DTOs.Auth;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Services;

namespace Library.Infrastructure.Services { 
    public class AuthService : IAuthService {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IRecuperacaoSenhaRepository _recuperacaoSenhaRepository;
        private readonly IEmailService _emailService;

        public AuthService(IUsuariosRepository usuariosRepository, IPasswordHasher passwordHasher, IJwtService jwtService, IRecuperacaoSenhaRepository recuperacaoSenhaRepository, IEmailService emailService) {
            _usuariosRepository = usuariosRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _recuperacaoSenhaRepository = recuperacaoSenhaRepository;
            _emailService = emailService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request) {
            if (string.IsNullOrWhiteSpace(request.dsLogin)) {
                throw new ArgumentException("O login é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(request.dsSenha)) {
                throw new ArgumentException("A senha é obrigatória.");
            }

            var usuario = await _usuariosRepository.ObterPorLoginAsync(
                request.dsLogin.Trim()
            );

            if (usuario is null) {
                throw new UnauthorizedAccessException("Login ou senha inválidos.");
            }

            if (!usuario.X_ATIVO) {
                throw new UnauthorizedAccessException("Usuário inativo.");
            }

            if (!_passwordHasher.Verify(request.dsSenha, usuario.DS_SENHA)) {
                throw new UnauthorizedAccessException("Login ou senha inválidos.");
            }

            var token = _jwtService.GerarToken(
                usuario.CD_USUARIO,
                usuario.DS_USUARIO,
                usuario.DS_LOGIN,
                usuario.DS_EMAIL,
                usuario.X_ADMINISTRADOR
            );

            return new LoginResponse {
                Token = token,

                Usuario = new UsuarioAuthDto {
                    CD_USUARIO = usuario.CD_USUARIO,
                    DS_USUARIO = usuario.DS_USUARIO,
                    DS_LOGIN = usuario.DS_LOGIN,
                    DS_EMAIL = usuario.DS_EMAIL,
                    X_ADMINISTRADOR = usuario.X_ADMINISTRADOR
                }
            };
        }

        public async Task<Usuarios?> ObterUsuarioAutenticadoAsync(int id) {
            if (id <= 0) {
                return null;
            }

            return await _usuariosRepository.ObterPorIdAsync(id);
        }

        public async Task RedefinirSenhaAsync(ResetPasswordRequest request) {
            if (string.IsNullOrWhiteSpace(request.Token)) {
                throw new ArgumentException("O token é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(request.NovaSenha)) {
                throw new ArgumentException("A nova senha é obrigatória.");
            }

            if (request.NovaSenha.Length < 8) {
                throw new ArgumentException("A nova senha deve possuir pelo menos 8 caracteres.");
            }

            var recuperacao = await _recuperacaoSenhaRepository
                .ObterPorTokenAsync(request.Token);

            if (recuperacao is null) {
                throw new UnauthorizedAccessException("Token inválido ou expirado.");
            }

            if (recuperacao.DT_UTILIZACAO.HasValue) {
                throw new UnauthorizedAccessException("Este token já foi utilizado.");
            }

            if (recuperacao.DT_EXPIRACAO <= DateTime.Now) {
                throw new UnauthorizedAccessException("Este token expirou.");
            }

            var usuario = await _usuariosRepository.ObterPorIdAsync(recuperacao.CD_USUARIO);

            if (usuario is null) {
                throw new UnauthorizedAccessException("Usuário não encontrado.");
            }

            if (!usuario.X_ATIVO) {
                throw new UnauthorizedAccessException("Usuário inativo.");
            }

            var senhaHash = _passwordHasher.Hash(request.NovaSenha);

            var senhaAtualizada = await _usuariosRepository.AtualizarSenhaAsync(usuario.CD_USUARIO, senhaHash);

            if (!senhaAtualizada) {
                throw new InvalidOperationException("Não foi possível atualizar a senha.");
            }

            var tokenUtilizado = await _recuperacaoSenhaRepository.MarcarComoUtilizadoAsync(recuperacao.CD_RECUPERACAO);

            if (!tokenUtilizado) {
                throw new InvalidOperationException("Não foi possível finalizar a recuperação da senha.");
            }
        }

        public async Task SolicitarRecuperacaoSenhaAsync(string email) {
            if (string.IsNullOrWhiteSpace(email)) {
                throw new ArgumentException("O e-mail é obrigatório.");
            }

            var usuario = await _usuariosRepository.ObterPorEmailAsync(
                email.Trim()
            );

            if (usuario is null) {
                throw new UnauthorizedAccessException(
                    "Não foi possível realizar a recuperação da senha."
                );
            }

            if (!usuario.X_ATIVO) {
                throw new UnauthorizedAccessException(
                    "Não foi possível realizar a recuperação da senha."
                );
            }

            await _recuperacaoSenhaRepository.InvalidarTokensAnterioresAsync(usuario.CD_USUARIO);

            var token = Guid.NewGuid().ToString("N");

            var recuperacao = new RecuperacaoSenha {
                CD_USUARIO = usuario.CD_USUARIO,
                DS_TOKEN = token,
                DT_EXPIRACAO = DateTime.Now.AddHours(1),
                DT_CADASTRO = DateTime.Now
            };

            await _recuperacaoSenhaRepository.CriarAsync(recuperacao);

            var link = $"http://localhost:3000/redefinir-senha?token={token}";

            var assunto = "Recuperação de senha - Library";

            var corpo = $"""
                <!DOCTYPE html>
                <html lang="pt-BR">
                    <head>
                        <meta charset="UTF-8">
                        <title>Redefinição de senha</title>
                    </head>
                    <body style="margin:0; padding:0; background-color:#eef3f8; font-family: Arial, Helvetica, sans-serif;">
                        <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="background-color:#eef3f8; padding:30px 0;">
                            <tr>
                                <td align="center">

                                    <table role="presentation" width="480" cellpadding="0" cellspacing="0" style="background-color:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 2px 8px rgba(0,0,0,0.08);">

                                        <!-- Cabeçalho -->
                                        <tr>
                                            <td align="center" style="background-color:#0b3d66; padding:28px 20px;">
                                                <h1 style="margin:0; color:#ffffff; font-size:20px; font-weight:600;">
                                                    Redefinição de Senha
                                                </h1>
                                            </td>
                                        </tr>

                                        <!-- Corpo -->
                                        <tr>
                                            <td style="padding:32px 36px; color:#1f2d3d;">
                                                <p style="margin:0 0 16px; font-size:15px; line-height:1.6;">
                                                    Olá, <strong>{usuario.DS_USUARIO}</strong>.
                                                </p>

                                                <p style="margin:0 0 16px; font-size:15px; line-height:1.6;">
                                                    Recebemos uma solicitação para redefinir a senha da sua conta.
                                                    Para continuar, clique no botão abaixo e crie uma nova senha:
                                                </p>

                                                <table role="presentation" cellpadding="0" cellspacing="0" style="margin:24px auto;">
                                                    <tr>
                                                        <td align="center" bgcolor="#1565a8" style="border-radius:6px;">
                                                            <a href="{link}"
                                                            style="display:inline-block; padding:13px 28px; font-size:15px; font-weight:600; color:#ffffff; text-decoration:none; border-radius:6px; background-color:#1565a8;">
                                                                Redefinir minha senha
                                                            </a>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <p style="margin:0 0 16px; font-size:14px; line-height:1.6; color:#4a5b6c;">
                                                    Este link é válido por <strong>30 minutos</strong>. Após esse período, será necessário solicitar uma nova redefinição.
                                                </p>

                                                <p style="margin:0; font-size:14px; line-height:1.6; color:#4a5b6c;">
                                                    Se você não solicitou essa alteração, pode ignorar este e-mail com segurança. Sua senha permanecerá inalterada.
                                                </p>
                                            </td>
                                        </tr>

                                        <!-- Rodapé -->
                                        <tr>
                                            <td align="center" style="background-color:#eaf1f8; padding:18px 20px;">
                                                <p style="margin:0; font-size:12px; color:#6b7f92;">
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
             """;

            await _emailService.EnviarAsync(
                usuario.DS_EMAIL,
                assunto,
                corpo
            );
        }

        public async Task<bool> ValidarTokenRecuperacaoAsync(string token) {
            if (string.IsNullOrWhiteSpace(token)) {
                return false;
            }

            var recuperacao = await _recuperacaoSenhaRepository
                .ObterPorTokenAsync(token);

            if (recuperacao is null) {
                return false;
            }

            if (recuperacao.DT_UTILIZACAO.HasValue) {
                return false;
            }

            if (recuperacao.DT_EXPIRACAO <= DateTime.Now) {
                return false;
            }

            return true;
        }
    }
}