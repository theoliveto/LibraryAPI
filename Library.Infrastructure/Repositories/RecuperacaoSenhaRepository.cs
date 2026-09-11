using Dapper;
using Library.Infrastructure.Data;
using Library.Domain.Entities.Auth;
using Library.Domain.Interfaces.Auth;

namespace Library.Infrastructure.Repositories {
    public class RecuperacaoSenhaRepository : IRecuperacaoSenhaRepository {
        private readonly DatabaseConnection _database;

        public RecuperacaoSenhaRepository(DatabaseConnection database) {
            _database = database;
        }

        public async Task<int> CriarAsync(RecuperacaoSenha recuperacao) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                INSERT INTO RecuperacaoSenha
                (
                    usuarioId
                    , token
                    , expiracao
                    , cadastro
                )
                VALUES
                (
                    @usuarioId
                    , @token
                    , @expiracao
                    , @cadastro
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, recuperacao);
        }

        public async Task<RecuperacaoSenha?> ObterPorTokenAsync(string token) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                SELECT
                    id
                    , usuarioId
                    , token
                    , expiracao
                    , utilizacao
                    , cadastro
                FROM
                    RecuperacaoSenha
                WHERE
                    token = @token
            ";

            return await connection.QueryFirstOrDefaultAsync<RecuperacaoSenha>(sql, new { token = token });
        }

        public async Task<bool> MarcarComoUtilizadoAsync(int id) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE
                    RecuperacaoSenha
                SET
                    utilizacao = @utilizacao
                WHERE
                    id = @id
                    AND utilizacao IS NULL
            ";

            var linhas = await connection.ExecuteAsync(
                sql,
                new { id = id, utilizacao = DateTime.Now }
            );

            return linhas > 0;
        }

        public async Task InvalidarTokensAnterioresAsync(int cdUsuario) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE
                    RecuperacaoSenha
                SET
                    utilizacao = @utilizacao
                WHERE
                    id = @id
                    AND utilizacao IS NULL
            ";

            await connection.ExecuteAsync(
                sql,
                new { id = cdUsuario, utilizacao = DateTime.Now }
            );
        }
    }
}