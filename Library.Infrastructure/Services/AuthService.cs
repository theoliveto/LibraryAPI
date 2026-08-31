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

        public AuthService(IUsuariosRepository usuariosRepository, IPasswordHasher passwordHasher, IJwtService jwtService, IRecuperacaoSenhaRepository recuperacaoSenhaRepository) {
            _usuariosRepository = usuariosRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _recuperacaoSenhaRepository = recuperacaoSenhaRepository;
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

            await _recuperacaoSenhaRepository
                .InvalidarTokensAnterioresAsync(usuario.CD_USUARIO);

            var token = Guid.NewGuid().ToString("N");

            var recuperacao = new RecuperacaoSenha {
                CD_USUARIO = usuario.CD_USUARIO,
                DS_TOKEN = token,
                DT_EXPIRACAO = DateTime.Now.AddHours(1),
                DT_CADASTRO = DateTime.Now
            };

            await _recuperacaoSenhaRepository.CriarAsync(recuperacao);
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