using Library.Domain.Entities;

namespace Library.Domain.Interfaces {
    public interface IUsuariosRepository {
        Task<IEnumerable<Usuarios>> ObterTodosAsync();

        Task<Usuarios?> ObterPorIdAsync(int id);

        Task<Usuarios?> ObterPorLoginAsync(string login);

        Task<int> CriarAsync(Usuarios pessoa);

        Task<bool> AtualizarAsync(Usuarios pessoa);

        Task<bool> ExcluirAsync(int id);
    }
}