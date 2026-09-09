using Library.Domain.Entities;

namespace Library.Domain.Interfaces {
    public interface ICategoriasRepository {
        Task<IEnumerable<Categorias>> ObterTodosAsync();

        Task<Categorias?> ObterPorIdAsync(int id);

        Task<int> CriarAsync(Categorias categoria);

        Task<bool> AtualizarAsync(Categorias categoria);

        Task<bool> ExcluirAsync(int id);
    }
}