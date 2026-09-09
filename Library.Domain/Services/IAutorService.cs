using Library.Domain.Entities;
using Library.Domain.DTOs.Autor;

namespace Library.Domain.Services {
    public interface IAutorService {
        Task<IEnumerable<Autor>> ObterTodosAsync();

        Task<Autor?> ObterPorIdAsync(int id);

        Task<int> CriarAsync(PostRequest request);

        Task<bool> AtualizarAsync(int id, PutRequest request);

        Task<bool> ExcluirAsync(int id);
    }
}