using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class CategoriasRepository : ICategoriasRepository {
        private readonly DatabaseConnection _database;

        public CategoriasRepository(DatabaseConnection database) {
            _database = database;
        }

        public async Task<IEnumerable<Categorias>> ObterTodosAsync() {
            using var connection = _database.CreateConnection();

            const string sql = @"
                SELECT
                    CD_CATEGORIA 	
                    , DS_CATEGORIA	
                    , DS_OBSERVACOES 	
                    , X_ATIVO 		
                    , DT_CADASTRO 	
                    , DT_ATUALIZACAO 	
                FROM 
                    TBL_CATEGORIAS
                ORDER BY 
                    DS_CATEGORIA
            ";

            return await connection.QueryAsync<Categorias>(sql);
        }

        public async Task<Categorias?> ObterPorIdAsync(int id) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                SELECT
                    CD_CATEGORIA 	
                    , DS_CATEGORIA	
                    , DS_OBSERVACOES 	
                    , X_ATIVO 		
                    , DT_CADASTRO 	
                    , DT_ATUALIZACAO 	
                FROM 
                    TBL_CATEGORIAS
                WHERE 
                    CD_CATEGORIA = @CD_CATEGORIA
            ";

            return await connection.QueryFirstOrDefaultAsync<Categorias>(sql, new { CD_CATEGORIA = id });
        }

        public async Task<int> CriarAsync(Categorias categorias) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                INSERT INTO TBL_CATEGORIAS
                (
                    DS_CATEGORIA 	
                    , DS_OBSERVACOES 	
                    , X_ATIVO 		
                    , DT_CADASTRO 	
                    , DT_ATUALIZACAO
                )
                VALUES
                (
                    @DS_CATEGORIA 	
                    , @DS_OBSERVACOES 	
                    , @X_ATIVO 		
                    , @DT_CADASTRO 	
                    , @DT_ATUALIZACAO
                );
            ";

            return await connection.ExecuteScalarAsync<int>(sql, categorias);
        }

        public async Task<bool> AtualizarAsync(Categorias categorias) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE 
                    TBL_CATEGORIAS
                SET
                    DS_CATEGORIA = @DS_CATEGORIA
                    , DS_OBSERVACOES = @DS_OBSERVACOES 	
                    , X_ATIVO = @X_ATIVO	
                    , DT_ATUALIZACAO = @DT_ATUALIZACAO
                WHERE 
                    CD_CATEGORIA = @CD_CATEGORIA
            ";

            var linhas = await connection.ExecuteAsync(sql, categorias);

            return linhas > 0;
        }

        public async Task<bool> ExcluirAsync(int id) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                DELETE FROM TBL_CATEGORIAS
                WHERE CD_CATEGORIA = @CD_CATEGORIA
            ";

            var linhas = await connection.ExecuteAsync(sql, new { CD_CATEGORIA = id });

            return linhas > 0;
        }
    }
}