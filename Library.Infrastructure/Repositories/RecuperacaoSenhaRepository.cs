using Dapper;
using Library.Domain.Entities.Auth;
using Library.Domain.Interfaces.Auth;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class RecuperacaoSenhaRepository : IRecuperacaoSenhaRepository {
        private readonly DatabaseConnection _database;

        public RecuperacaoSenhaRepository(DatabaseConnection database) {
            _database = database;
        }

        public async Task<int> CriarAsync(RecuperacaoSenha recuperacao) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                INSERT INTO TBL_RECUPERACAO_SENHA
                (
                    CD_USUARIO,
                    DS_TOKEN,
                    DT_EXPIRACAO,
                    DT_CADASTRO
                )
                VALUES
                (
                    @CD_USUARIO,
                    @DS_TOKEN,
                    @DT_EXPIRACAO,
                    @DT_CADASTRO
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, recuperacao);
        }

        public async Task<RecuperacaoSenha?> ObterPorTokenAsync(string token) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                SELECT
                    CD_RECUPERACAO,
                    CD_USUARIO,
                    DS_TOKEN,
                    DT_EXPIRACAO,
                    DT_UTILIZACAO,
                    DT_CADASTRO
                FROM
                    TBL_RECUPERACAO_SENHA
                WHERE
                    DS_TOKEN = @DS_TOKEN
            ";

            return await connection.QueryFirstOrDefaultAsync<RecuperacaoSenha>(sql, new { DS_TOKEN = token });
        }

        public async Task<bool> MarcarComoUtilizadoAsync(int id) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE
                    TBL_RECUPERACAO_SENHA
                SET
                    DT_UTILIZACAO = @DT_UTILIZACAO
                WHERE
                    CD_RECUPERACAO = @CD_RECUPERACAO
                    AND DT_UTILIZACAO IS NULL
            ";

            var linhas = await connection.ExecuteAsync(
                sql,
                new {
                    CD_RECUPERACAO = id,
                    DT_UTILIZACAO = DateTime.Now
                }
            );

            return linhas > 0;
        }

        public async Task InvalidarTokensAnterioresAsync(int cdUsuario) {
            using var connection = _database.CreateConnection();

            const string sql = @"
                UPDATE
                    TBL_RECUPERACAO_SENHA
                SET
                    DT_UTILIZACAO = @DT_UTILIZACAO
                WHERE
                    CD_USUARIO = @CD_USUARIO
                    AND DT_UTILIZACAO IS NULL
            ";

            await connection.ExecuteAsync(
                sql,
                new {
                    CD_USUARIO = cdUsuario,
                    DT_UTILIZACAO = DateTime.Now
                }
            );
        }
    }
}