using Library.Domain.Services;
using Library.Domain.Interfaces;

namespace Library.Infrastructure.Services {
    public abstract class ServiceBase<TEntity, TPostRequest, TPutRequest> 
        : IServiceBase<TEntity, TPostRequest, TPutRequest> where TEntity : class {

        protected readonly IRepositoryBase<TEntity> Repository;

        protected ServiceBase(IRepositoryBase<TEntity> repository) {
            Repository = repository;
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodosAsync() {
            return await Repository.ObterTodosAsync();
        }

        public virtual async Task<TEntity?> ObterPorIdAsync(int id) {
            return await Repository.ObterPorIdAsync(id);
        }

        public virtual async Task<int> CriarAsync(TPostRequest request) {
            var entity = MapearParaCriacao(request);
            return await Repository.CriarAsync(entity);
        }

        public virtual async Task<bool> AtualizarAsync(int id, TPutRequest request) {
            var existente = await Repository.ObterPorIdAsync(id);
            if (existente is null) {
                return false;
            }

            var entity = MapearParaAtualizacao(id, request, existente);
            return await Repository.AtualizarAsync(entity);
        }

        public virtual async Task<bool> ExcluirAsync(int id) {
            return await Repository.ExcluirAsync(id);
        }

        protected abstract TEntity MapearParaCriacao(TPostRequest request);

        protected abstract TEntity MapearParaAtualizacao(int id, TPutRequest request, TEntity existente);
    }
}