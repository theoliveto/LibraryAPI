namespace Library.Domain.Interfaces {
    public interface IRepositoryBase<TEntity> where TEntity : class {
        Task<IEnumerable<TEntity>> ObterTodosAsync();
        Task<TEntity?> ObterPorIdAsync(int id);
        Task<int> CriarAsync(TEntity entity);
        Task<bool> AtualizarAsync(TEntity entity);
        Task<bool> ExcluirAsync(int id);
    }
}