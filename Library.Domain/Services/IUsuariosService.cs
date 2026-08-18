using Library.Domain.Entities;
using Library.Domain.DTOs.Usuarios;

namespace Library.Domain.Services {
    public interface IUsuariosService {
        Task<IEnumerable<Usuarios>> ObterTodosAsync();

        Task<Usuarios?> ObterPorIdAsync(int id);

        Task<int> CriarAsync(PostRequest request);

        Task<bool> AtualizarAsync(int id, PutRequest request);

        Task<bool> ExcluirAsync(int id);
    }
}