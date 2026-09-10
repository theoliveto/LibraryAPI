using Library.Domain.Entities;

namespace Library.Domain.Interfaces {
    public interface IUsuariosRepository : IRepositoryBase<Usuarios> {
        Task<Usuarios?> ObterPorLoginAsync(string login);

        Task<Usuarios?> ObterPorEmailAsync(string email);

        Task<bool> AtualizarSenhaAsync(int cdUsuario, string senhaHash);

        Task IncrementarTentativaLoginAsync(int id);

        Task ZerarTentativasLoginAsync(int id);

        Task BloquearLoginAsync(int id, DateTime dataBloqueio);
    }
}