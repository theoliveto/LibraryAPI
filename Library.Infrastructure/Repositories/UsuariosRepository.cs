using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {  
    public class UsuariosRepository : IUsuariosRepository {
        private readonly DatabaseConnection _database;

        public UsuariosRepository(DatabaseConnection database) {
            _database = database;
        }

        public async Task<IEnumerable<Usuarios>> ObterTodosAsync() {
            using var connection = _database.CreateConnection();

            const string sql = @"
                SELECT
                    CD_USUARIO 	
                    , DS_USUARIO 	
                    , DS_LOGIN 		
                    , DS_EMAIL 	
                    , DS_SENHA 		
                    , DS_OBSERVACOES 	
                    , X_ADMINISTRADOR
                    , X_ATIVO 		
                    , DT_CADASTRO 	
                    , DT_ATUALIZACAO 	
                FROM 
                    TBL_USUARIOS
                ORDER BY 
                    DS_USUARIO
             ";

            return await connection.QueryAsync<Usuarios>(sql);
        }

        public async Task<Usuarios?> ObterPorIdAsync(int id) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                SELECT
                    CD_USUARIO 	
                    , DS_USUARIO 	
                    , DS_LOGIN 		
                    , DS_EMAIL 	
                    , DS_SENHA 		
                    , DS_OBSERVACOES 	
                    , X_ADMINISTRADOR
                    , X_ATIVO 		
                    , DT_CADASTRO 	
                    , DT_ATUALIZACAO 	
                FROM 
                    TBL_USUARIOS
                WHERE 
                    CD_USUARIO = @CD_USUARIO
            ";

            return await connection.QueryFirstOrDefaultAsync<Usuarios>(sql, new { CD_USUARIO = id });
        }

        public async Task<int> CriarAsync(Usuarios usuario) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                INSERT INTO TBL_USUARIOS
                (
                    CD_USUARIO 	
                    , DS_USUARIO 	
                    , DS_LOGIN 		
                    , DS_EMAIL 	
                    , DS_SENHA 		
                    , DS_OBSERVACOES 	
                    , X_ADMINISTRADOR
                    , X_ATIVO 		
                    , DT_CADASTRO 	
                    , DT_ATUALIZACAO 
                )
                VALUES
                (
                    @CD_USUARIO 	
                    , @DS_USUARIO 	
                    , @DS_LOGIN 		
                    , @DS_EMAIL 	
                    , @DS_SENHA 		
                    , @DS_OBSERVACOES 	
                    , @X_ADMINISTRADOR
                    , @X_ATIVO 		
                    , @DT_CADASTRO 	
                    , @DT_ATUALIZACAO 
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, usuario);
        }

        public async Task<bool> AtualizarAsync(Usuarios usuario) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE 
                    TBL_USUARIOS
                SET
                    DS_USUARIO = @DS_USUARIO
                    , DS_LOGIN = @DS_LOGIN
                    , DS_EMAIL = @DS_EMAIL
                    , DS_SENHA = @DS_SENHA
                    , DS_OBSERVACOES = @DS_OBSERVACOES
                    , X_ADMINISTRADOR = @X_ADMINISTRADOR
                    , X_ATIVO = @X_ATIVO
                    , DT_ATUALIZACAO = @DT_ATUALIZACAO
                WHERE 
                    CD_USUARIO = @CD_USUARIO
            ";

            var linhas = await connection.ExecuteAsync(sql, usuario);

            return linhas > 0;
        }

        public async Task<bool> ExcluirAsync(int id) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                DELETE FROM TBL_USUARIOS
                WHERE CD_USUARIO = @CD_USUARIO
            ";

            var linhas = await connection.ExecuteAsync(sql, new { CD_USUARIO = id });

            return linhas > 0;
        }

        public async Task<Usuarios?> ObterPorLoginAsync(string login) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                SELECT
                    CD_USUARIO
                    , DS_USUARIO
                    , DS_LOGIN
                    , DS_EMAIL
                    , DS_SENHA
                    , DS_OBSERVACOES
                    , X_ADMINISTRADOR
                    , X_ATIVO
                    , DT_CADASTRO
                    , DT_ATUALIZACAO
                FROM
                    TBL_USUARIOS
                WHERE
                    DS_LOGIN = @DS_LOGIN
            ";

            return await connection.QueryFirstOrDefaultAsync<Usuarios>(sql, new { DS_LOGIN = login } );
        }

        public async Task<Usuarios?> ObterPorEmailAsync(string email) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                SELECT
                    CD_USUARIO
                    , DS_USUARIO
                    , DS_LOGIN
                    , DS_EMAIL
                    , DS_SENHA
                    , DS_OBSERVACOES
                    , X_ADMINISTRADOR
                    , X_ATIVO
                    , DT_CADASTRO
                    , DT_ATUALIZACAO
                FROM
                    TBL_USUARIOS
                WHERE
                    DS_EMAIL = @DS_EMAIL
            ";

            return await connection.QueryFirstOrDefaultAsync<Usuarios>(
                sql,
                new { DS_EMAIL = email }
            );
        }

        public async Task<bool> AtualizarSenhaAsync(int cdUsuario, string senhaHash) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE
                    TBL_USUARIOS
                SET
                    DS_SENHA = @DS_SENHA,
                    DT_ATUALIZACAO = @DT_ATUALIZACAO
                WHERE
                    CD_USUARIO = @CD_USUARIO
            ";

            var linhas = await connection.ExecuteAsync(
                sql,
                new {
                    CD_USUARIO = cdUsuario,
                    DS_SENHA = senhaHash,
                    DT_ATUALIZACAO = DateTime.Now
                }
            );

            return linhas > 0;
        }

        public async Task IncrementarTentativaLoginAsync(int id) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE 
                    TBL_USUARIOS
                SET 
                    NR_TENTATIVAS_LOGIN = NR_TENTATIVAS_LOGIN + 1
                WHERE 
                    CD_USUARIO = @CD_USUARIO
            ";

            await connection.ExecuteAsync(sql, new { CD_USUARIO = id });
        }

        public async Task ZerarTentativasLoginAsync(int id) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE 
                    TBL_USUARIOS
                SET
                    NR_TENTATIVAS_LOGIN = 0,
                    DT_BLOQUEIO_LOGIN = NULL
                WHERE 
                    CD_USUARIO = @CD_USUARIO
            ";

            await connection.ExecuteAsync(sql, new {
                CD_USUARIO = id
            });
        }

        public async Task BloquearLoginAsync(int id, DateTime dataBloqueio) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE 
                    TBL_USUARIOS
                SET
                    DT_BLOQUEIO_LOGIN = @DT_BLOQUEIO_LOGIN
                WHERE 
                    CD_USUARIO = @CD_USUARIO
            ";

            await connection.ExecuteAsync(sql, new {
                CD_USUARIO = id,
                DT_BLOQUEIO_LOGIN = dataBloqueio 
            });
        }
    }
}