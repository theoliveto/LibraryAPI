using Library.Domain.DTOs.Auth;
using Library.Domain.Entities;

namespace Library.Domain.Services; 
public interface IAuthService {
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<Usuarios?> ObterUsuarioAutenticadoAsync(int id);
}