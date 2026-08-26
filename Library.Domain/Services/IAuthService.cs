using Library.Domain.DTOs.Auth;

namespace Library.Domain.Services; 
public interface IAuthService {
    Task<LoginResponse> LoginAsync(LoginRequest request);
}