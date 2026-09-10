namespace Library.Domain.Services {
    public interface IServiceBase<TEntity, TPostRequest, TPutRequest> where TEntity : class {
        Task<IEnumerable<TEntity>> ObterTodosAsync();
        Task<TEntity?> ObterPorIdAsync(int id);
        Task<int> CriarAsync(TPostRequest request);
        Task<bool> AtualizarAsync(int id, TPutRequest request);
        Task<bool> ExcluirAsync(int id);
    }
}