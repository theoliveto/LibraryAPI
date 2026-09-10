using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class EditorasRepository : RepositoryBase<Editoras>, IEditorasRepository {
        protected override string TableName => "Editoras";
        protected override string PrimaryKeyColumn => "id";

        public EditorasRepository(DatabaseConnection database) : base(database) { }

        public override async Task<int> CriarAsync(Editoras editora) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                INSERT INTO Editoras
                (
                    descricao
                    , razaoSocial
                    , cpfCnpj
                    , email
                    , celular
                    , telefone
                    , nacionalidade
                    , siteOficial
                    , fundacao
                    , cep
                    , endereco
                    , cidade
                    , estado
                    , bairro
                    , numero
                    , complemento
                    , observacoes
                    , ativo
                    , cadastro
                    , atualizacao
                )
                VALUES
                (
                    @descricao
                    , @razaoSocial
                    , @cpfCnpj
                    , @email
                    , @celular
                    , @telefone
                    , @nacionalidade
                    , @siteOficial
                    , @fundacao
                    , @cep
                    , @endereco
                    , @cidade
                    , @estado
                    , @bairro
                    , @numero
                    , @complemento
                    , @observacoes
                    , @ativo
                    , @cadastro
                    , @atualizacao
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, editora);
        }

        public override async Task<bool> AtualizarAsync(Editoras editora) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE Editoras
                SET
                    descricao = @descricao
                    , razaoSocial = @razaoSocial
                    , cpfCnpj = @cpfCnpj
                    , email = @email
                    , celular = @celular
                    , telefone = @telefone
                    , nacionalidade = @nacionalidade
                    , siteOficial = @siteOficial
                    , fundacao = @fundacao
                    , cep = @cep
                    , endereco = @endereco
                    , cidade = @cidade
                    , estado = @estado
                    , bairro = @bairro
                    , numero = @numero
                    , complemento = @complemento
                    , observacoes = @observacoes
                    , ativo = @ativo
                    , cadastro = @cadastro
                    , atualizacao = @atualizacao
                WHERE
                    id = @id
            ";

            var linhas = await connection.ExecuteAsync(sql, editora);
            return linhas > 0;
        }
    }
}