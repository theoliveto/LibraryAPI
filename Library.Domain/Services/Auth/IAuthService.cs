using Library.Domain.Entities;
using Library.Domain.DTOs.Auth;

namespace Library.Domain.Services.Auth { 
    public interface IAuthService {
        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task<Usuarios?> ObterUsuarioAutenticadoAsync(int id);

        Task SolicitarRecuperacaoSenhaAsync(string email);

        Task<bool> ValidarTokenRecuperacaoAsync(string token);

        Task RedefinirSenhaAsync(ResetPasswordRequest request);
    }
}