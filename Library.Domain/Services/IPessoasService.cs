using Library.Domain.DTOs.Pessoas;
using Library.Domain.Entities;

namespace Library.Domain.Services {
    public interface IPessoasService {
        Task<IEnumerable<Pessoas>> ObterTodosAsync();

        Task<Pessoas?> ObterPorIdAsync(int id);

        Task<int> CriarAsync(PostRequest request);

        Task<bool> AtualizarAsync(int id, PutRequest request);

        Task<bool> ExcluirAsync(int id);
    }
}