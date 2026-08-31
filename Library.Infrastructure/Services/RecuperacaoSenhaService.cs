using System.Text;
using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using System.Security.Cryptography;

namespace Library.Infrastructure.Services {
    public class RecuperacaoSenhaService : IRecuperacaoSenhaService {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IRecuperacaoSenhaRepository _passwordResetRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public RecuperacaoSenhaService(
            IUsuariosRepository usuariosRepository, IRecuperacaoSenhaRepository passwordResetRepository, IPasswordHasher passwordHasher, IEmailService emailService, IConfiguration configuration) {
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
                <html>
                    <body>
                        <h2>Redefinição de senha</h2>

                        <p>Olá, {usuario.DS_USUARIO}.</p>

                        <p>Recebemos uma solicitação para redefinir sua senha.</p>

                        <p>Clique no botão abaixo para criar uma nova senha:</p>

                        <p><a href=""{link}"">Redefinir minha senha</a></p>

                        <p>Este link será válido por 30 minutos.</p>

                        <p>Se você não solicitou a redefinição da senha, pode ignorar este e-mail.</p>
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