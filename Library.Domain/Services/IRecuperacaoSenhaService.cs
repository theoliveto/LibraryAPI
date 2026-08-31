namespace Library.Domain.Services {
    public interface IRecuperacaoSenhaService {
        Task SolicitarRecuperacaoAsync(string email);
        Task<bool> ValidarTokenAsync(string token);
        Task<bool> RedefinirSenhaAsync(string token, string novaSenha);
    }
}