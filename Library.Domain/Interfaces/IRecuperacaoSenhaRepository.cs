using Library.Domain.Entities;

namespace Library.Domain.Interfaces { 
    public interface IRecuperacaoSenhaRepository {
        Task<int> CriarAsync(RecuperacaoSenha recuperacao);
        Task<RecuperacaoSenha?> ObterPorTokenAsync(string token);
        Task<bool> MarcarComoUtilizadoAsync(int id);
        Task InvalidarTokensAnterioresAsync(int cdUsuario);
    }
}