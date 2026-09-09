using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class AutoresRepository : IAutorRepository {
        private readonly DatabaseConnection _database;

        public AutoresRepository(DatabaseConnection database) {
            _database = database;
        }

        public async Task<IEnumerable<Autor>> ObterTodosAsync() {
            using var connection = _database.CreateConnection();

            const string sql = @"
                SELECT
                    CD_AUTOR 	
                    , DS_AUTOR 	
                    , DT_NASCIMENTO 		
                    , DT_FALECIMENTO 	
                    , DS_PAIS 
                    , DS_GENERO
                    , DS_SITEOFICIAL 
                    , IM_FOTO
                    , DS_OBSERVACOES 	
                    , X_ATIVO 		
                    , DT_CADASTRO 	
                    , DT_ATUALIZACAO 	
                FROM 
                    TBL_AUTORES
                ORDER BY 
                    DS_AUTOR
            ";

            return await connection.QueryAsync<Autor>(sql);
        }

        public async Task<Autor?> ObterPorIdAsync(int id) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                SELECT
                    CD_AUTOR 	
                    , DS_AUTOR 	
                    , DT_NASCIMENTO 		
                    , DT_FALECIMENTO 	
                    , DS_PAIS 
                    , DS_GENERO
                    , DS_SITEOFICIAL 
                    , IM_FOTO
                    , DS_OBSERVACOES 	
                    , X_ATIVO 		
                    , DT_CADASTRO 	
                    , DT_ATUALIZACAO	
                FROM 
                    TBL_AUTORES
                WHERE 
                    CD_AUTOR = @CD_AUTOR
            ";

            return await connection.QueryFirstOrDefaultAsync<Autor>(sql, new { CD_AUTOR = id });
        }

        public async Task<int> CriarAsync(Autor autor) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                INSERT INTO TBL_AUTORES
                (
                    DS_AUTOR 	
                    , DT_NASCIMENTO 		
                    , DT_FALECIMENTO 	
                    , DS_PAIS 
                    , DS_GENERO
                    , DS_SITEOFICIAL 
                    , IM_FOTO
                    , DS_OBSERVACOES 	
                    , X_ATIVO 		
                    , DT_CADASTRO 	
                    , DT_ATUALIZACAO
                )
                VALUES
                (
                    @DS_AUTOR 	
                    , @DT_NASCIMENTO 		
                    , @DT_FALECIMENTO 	
                    , @DS_PAIS 
                    , @DS_GENERO
                    , @DS_SITEOFICIAL 
                    , @IM_FOTO
                    , @DS_OBSERVACOES 	
                    , @X_ATIVO 		
                    , @DT_CADASTRO 	
                    , @DT_ATUALIZACAO
                );
            ";

            return await connection.ExecuteScalarAsync<int>(sql, autor);
        }

        public async Task<bool> AtualizarAsync(Autor autor) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE 
                    TBL_AUTORES
                SET
                    DS_AUTOR = @DS_AUTOR	
                    , DT_NASCIMENTO = @DT_NASCIMENTO		
                    , DT_FALECIMENTO = @DT_FALECIMENTO 	
                    , DS_PAIS = @DS_PAIS 
                    , DS_GENERO = @DS_GENERO
                    , DS_SITEOFICIAL = @DS_SITEOFICIAL 
                    , IM_FOTO = @IM_FOTO
                    , DS_OBSERVACOES = @DS_OBSERVACOES 	
                    , X_ATIVO = @X_ATIVO	
                    , DT_ATUALIZACAO = @DT_ATUALIZACAO
                WHERE 
                    CD_AUTOR = @CD_AUTOR
            ";

            var linhas = await connection.ExecuteAsync(sql, autor);

            return linhas > 0;
        }

        public async Task<bool> ExcluirAsync(int id) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                DELETE FROM TBL_AUTORES
                WHERE CD_AUTOR = @CD_AUTOR
            ";

            var linhas = await connection.ExecuteAsync(sql, new { CD_AUTOR = id });

            return linhas > 0;
        }
    }
}