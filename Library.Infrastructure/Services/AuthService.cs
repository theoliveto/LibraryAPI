using Library.Domain.Services;
using Library.Domain.DTOs.Auth;
using Library.Domain.Interfaces;

namespace Library.Infrastructure.Services; 
public class AuthService : IAuthService {
    private readonly IUsuariosRepository _usuariosRepository;

    public AuthService(IUsuariosRepository usuariosRepository) {
        _usuariosRepository = usuariosRepository;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request) {
        if (string.IsNullOrWhiteSpace(request.DS_LOGIN)) {
            throw new ArgumentException("O login é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(request.DS_SENHA)) {
            throw new ArgumentException("A senha é obrigatória.");
        }

        var usuario = await _usuariosRepository.ObterPorLoginAsync(
            request.DS_LOGIN.Trim()
        );

        if (usuario is null) {
            throw new UnauthorizedAccessException("Login ou senha inválidos.");
        }

        if (!usuario.X_ATIVO) {
            throw new UnauthorizedAccessException("Usuário inativo.");
        }

        if (usuario.DS_SENHA != request.DS_SENHA) {
            throw new UnauthorizedAccessException("Login ou senha inválidos.");
        }

        return new LoginResponse {
            Usuario = new UsuarioAuthDto {
                CD_USUARIO = usuario.CD_USUARIO,
                DS_USUARIO = usuario.DS_USUARIO,
                DS_LOGIN = usuario.DS_LOGIN,
                DS_EMAIL = usuario.DS_EMAIL,
                X_ADMINISTRADOR = usuario.X_ADMINISTRADOR
            }
        };
    }
}