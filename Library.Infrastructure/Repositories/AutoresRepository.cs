using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class AutoresRepository : RepositoryBase<Autores>, IAutoresRepository {
        protected override string TableName => "Autores";
        protected override string PrimaryKeyColumn => "id";

        public AutoresRepository(DatabaseConnection database) : base(database) { }

        public override async Task<int> CriarAsync(Autores autor) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                INSERT INTO Autores
                (
                    descricao
                    , nascimento
                    , falecimento
                    , nacionalidade
                    , genero
                    , siteOficial
                    , observacoes
                    , ativo
                    , cadastro
                    , atualizacao
                )
                VALUES
                (
                    @descricao
                    , @nascimento
                    , @falecimento
                    , @nacionalidade
                    , @genero
                    , @siteOficial
                    , @observacoes
                    , @ativo
                    , @cadastro
                    , @atualizacao
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, autor);
        }

        public override async Task<bool> AtualizarAsync(Autores autor) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE Autores
                SET
                    descricao = @descricao
                    , nascimento = @nascimento
                    , falecimento = @falecimento
                    , nacionalidade = @nacionalidade
                    , genero = @genero
                    , siteOficial = @siteOficial
                    , observacoes = @observacoes
                    , ativo = @ativo
                    , cadastro = @cadastro
                    , atualizacao = @atualizacao
                WHERE
                    id = @id
            ";

            var linhas = await connection.ExecuteAsync(sql, autor);
            return linhas > 0;
        }
    }
}