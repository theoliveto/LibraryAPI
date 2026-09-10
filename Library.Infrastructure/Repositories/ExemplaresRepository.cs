using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class ExemplaresRepository : RepositoryBase<Exemplares>, IExemplaresRepository {
        protected override string TableName => "Exemplares";
        protected override string PrimaryKeyColumn => "id";

        public ExemplaresRepository(DatabaseConnection database) : base(database) { }

        public override async Task<int> CriarAsync(Exemplares exemplar) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                INSERT INTO Exemplares
                (
                    descricao
                    , livroId
                    , patrimonio
                    , localizacao
                    , ean
                    , aquisicao
                    , valorAquisicao
                    , status
                    , observacoes
                    , ativo
                    , cadastro
                    , atualizacao
                )
                VALUES
                (
                    @descricao
                    , @livroId
                    , @patrimonio
                    , @localizacao
                    , @ean
                    , @aquisicao
                    , @valorAquisicao
                    , @status
                    , @observacoes
                    , @ativo
                    , @cadastro
                    , @atualizacao
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, exemplar);
        }

        public override async Task<bool> AtualizarAsync(Exemplares exemplar) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE Exemplares
                SET
                    descricao = @descricao
                    , livroId = @livroId
                    , patrimonio = @patrimonio
                    , localizacao = @localizacao
                    , ean = @ean
                    , aquisicao = @aquisicao
                    , valorAquisicao = @valorAquisicao
                    , status = @status
                    , observacoes = @observacoes
                    , ativo = @ativo
                    , cadastro = @cadastro
                    , atualizacao = @atualizacao
                WHERE
                    id = @id
            ";

            var linhas = await connection.ExecuteAsync(sql, exemplar);
            return linhas > 0;
        }
    }
}