using Library.Domain.Entities;
using Library.Domain.DTOs.Categorias;

namespace Library.Domain.Services {
    public interface ICategoriasService {
        Task<IEnumerable<Categorias>> ObterTodosAsync();

        Task<Categorias?> ObterPorIdAsync(int id);

        Task<int> CriarAsync(PostRequest request);

        Task<bool> AtualizarAsync(int id, PutRequest request);

        Task<bool> ExcluirAsync(int id);
    }
}