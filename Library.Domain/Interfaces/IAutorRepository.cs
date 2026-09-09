using Library.Domain.Entities;

namespace Library.Domain.Interfaces {
    public interface IAutorRepository {
        Task<IEnumerable<Autor>> ObterTodosAsync();

        Task<Autor?> ObterPorIdAsync(int id);

        Task<int> CriarAsync(Autor autor);

        Task<bool> AtualizarAsync(Autor autor);

        Task<bool> ExcluirAsync(int id);
    }
}