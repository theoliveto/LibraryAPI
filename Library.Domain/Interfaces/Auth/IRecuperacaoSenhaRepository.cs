using Library.Domain.Entities.Auth;

namespace Library.Domain.Interfaces.Auth { 
    public interface IRecuperacaoSenhaRepository {
        Task<int> CriarAsync(RecuperacaoSenha recuperacao);
        Task<RecuperacaoSenha?> ObterPorTokenAsync(string token);
        Task<bool> MarcarComoUtilizadoAsync(int id);
        Task InvalidarTokensAnterioresAsync(int cdUsuario);
    }
}