using Dapper;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class {
        protected readonly DatabaseConnection Database;
        protected abstract string TableName { get; }
        protected abstract string PrimaryKeyColumn { get; }

        protected RepositoryBase(DatabaseConnection database) {
            Database = database;
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodosAsync() {
            using var connection = Database.CreateConnection();
            var sql = $"SELECT * FROM {TableName}";
            return await connection.QueryAsync<TEntity>(sql);
        }

        public virtual async Task<TEntity?> ObterPorIdAsync(int id) {
            using var connection = Database.CreateConnection();
            var sql = $"SELECT * FROM {TableName} WHERE {PrimaryKeyColumn} = @Id";
            return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, new { Id = id });
        }

        public virtual async Task<bool> ExcluirAsync(int id) {
            using var connection = Database.CreateConnection();
            var sql = $"DELETE FROM {TableName} WHERE {PrimaryKeyColumn} = @Id";
            var linhas = await connection.ExecuteAsync(sql, new { Id = id });
            return linhas > 0;
        }

        public abstract Task<int> CriarAsync(TEntity entity);

        public abstract Task<bool> AtualizarAsync(TEntity entity);
    }
}