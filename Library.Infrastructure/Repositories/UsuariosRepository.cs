using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {  
    public class UsuariosRepository : RepositoryBase<Usuarios>, IUsuariosRepository {
        protected override string TableName => "Usuarios";
        protected override string PrimaryKeyColumn => "id";

        public UsuariosRepository(DatabaseConnection database) : base(database) { }

        public override async Task<int> CriarAsync(Usuarios usuario) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                INSERT INTO Usuarios
                (
                    descricao 	
                    , login 	
                    , email 	
                    , senha
                    , perfil 	
                    , avatar
                    , administrador 		
                    , tentativasLogin 	
                    , bloqueioLogin
                    , observacoes 		
                    , ativo 	
                    , cadastro
                    , atualizacao
                )
                VALUES
                (
                    @descricao 	
                    , @login 	
                    , @email 	
                    , @senha
                    , @perfil 	
                    , NULL
                    , @administrador 		
                    , 0	
                    , NULL
                    , @observacoes 		
                    , @ativo 	
                    , @cadastro
                    , @atualizacao
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, usuario);
        }

        public override async Task<bool> AtualizarAsync(Usuarios usuario) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE 
                    Usuarios
                SET
                    descricao = @descricao
                    , login = @login
                    , email = @email
                    , perfil = @perfil
                    , administrador = @administrador
                    , tentativasLogin = 0
                    , bloqueioLogin = @bloqueioLogin   
                    , observacoes = @observacoes
                    , ativo = @ativo
                    , atualizacao = @atualizacao
                WHERE 
                    id = @id
            ";

            var linhas = await connection.ExecuteAsync(sql, usuario);

            return linhas > 0;
        }

        public async Task<Usuarios?> ObterPorLoginAsync(string login) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                SELECT
                    id
                    , descricao
                    , login
                    , email
                    , senha
                    , perfil
                    , administrador
                    , observacoes 
                    , ativo
                    , cadastro
                    , atualizacao
                FROM
                    Usuarios
                WHERE
                    login = @login
            ";

            return await connection.QueryFirstOrDefaultAsync<Usuarios>(sql, new { login = login } );
        }

        public async Task<Usuarios?> ObterPorEmailAsync(string email) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                SELECT
                    id
                    , descricao
                    , login
                    , email
                    , senha
                    , perfil
                    , administrador
                    , observacoes 
                    , ativo
                    , cadastro
                    , atualizacao
                FROM
                    Usuarios
                WHERE
                    email = @email
            ";

            return await connection.QueryFirstOrDefaultAsync<Usuarios>(sql, new { email = email } );
        }

        public async Task<bool> AtualizarSenhaAsync(int cdUsuario, string senhaHash) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE
                    Usuarios
                SET
                    senha = @senha,
                    atualizacao = @atualizacao
                WHERE
                    id = @id
            ";

            var linhas = await connection.ExecuteAsync(
                sql,
                new {
                    id = cdUsuario,
                    senha = senhaHash,
                    atualizacao = DateTime.Now
                }
            );

            return linhas > 0;
        }

        public async Task IncrementarTentativaLoginAsync(int id) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE 
                    Usuarios
                SET 
                    tentativasLogin = tentativasLogin + 1
                WHERE 
                    id = @id
            ";

            await connection.ExecuteAsync(sql, new { id = id });
        }

        public async Task ZerarTentativasLoginAsync(int id) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE 
                    Usuarios
                SET
                    tentativasLogin = 0,
                    bloqueioLogin = NULL
                WHERE 
                    id = @id
            ";

            await connection.ExecuteAsync(sql, new { id = id });
        }

        public async Task BloquearLoginAsync(int id, DateTime dataBloqueio) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE 
                    Usuarios
                SET
                    bloqueioLogin = @bloqueioLogin
                WHERE 
                    id = @id
            ";

            await connection.ExecuteAsync(sql, new {
                id = id,
                bloqueioLogin = dataBloqueio 
            });
        }
    }
}