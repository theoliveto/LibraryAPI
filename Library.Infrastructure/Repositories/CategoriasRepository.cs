using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class CategoriasRepository : RepositoryBase<Categorias>, ICategoriasRepository {
        protected override string TableName => "Categorias";
        protected override string PrimaryKeyColumn => "id";

        public CategoriasRepository(DatabaseConnection database) : base(database) { }

        public override async Task<int> CriarAsync(Categorias categoria) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                INSERT INTO Categorias
                (
                    descricao
                    , observacoes
                    , ativo
                    , cadastro
                    , atualizacao
                )
                VALUES
                (
                    @descricao
                    , @observacoes
                    , @ativo
                    , @cadastro
                    , @atualizacao
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, categoria);
        }

        public override async Task<bool> AtualizarAsync(Categorias categoria) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE Categorias
                SET
                    descricao = @descricao
                    , observacoes = @observacoes
                    , ativo = @ativo
                    , cadastro = @cadastro
                    , atualizacao = @atualizacao
                WHERE
                    id = @id
            ";

            var linhas = await connection.ExecuteAsync(sql, categoria);
            return linhas > 0;
        }
    }
}