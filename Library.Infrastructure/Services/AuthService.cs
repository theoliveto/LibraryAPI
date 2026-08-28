using Library.Domain.DTOs.Auth;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Services;

namespace Library.Infrastructure.Services; 
public class AuthService : IAuthService {
    private readonly IUsuariosRepository _usuariosRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthService(IUsuariosRepository usuariosRepository, IPasswordHasher passwordHasher, IJwtService jwtService) {
        _usuariosRepository = usuariosRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
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
}