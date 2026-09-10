using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class LivrosRepository : RepositoryBase<Livros>, ILivrosRepository {
        protected override string TableName => "Livros";
        protected override string PrimaryKeyColumn => "id";

        public LivrosRepository(DatabaseConnection database) : base(database) { }

        public override async Task<int> CriarAsync(Livros livro) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                INSERT INTO Livros
                (
                    descricao
                    , subtitulo
                    , autorId
                    , editoraId
                    , categoriaId
                    , isbn
                    , idioma
                    , edicao
                    , paginas
                    , publicacao
                    , sinopse
                    , imagemCapa
                    , observacoes
                    , ativo
                    , cadastro
                    , atualizacao
                )
                VALUES
                (
                    @descricao
                    , @subtitulo
                    , @autorId
                    , @editoraId
                    , @categoriaId
                    , @isbn
                    , @idioma
                    , @edicao
                    , @paginas
                    , @publicacao
                    , @sinopse
                    , @imagemCapa
                    , @observacoes
                    , @ativo
                    , @cadastro
                    , @atualizacao
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, livro);
        }

        public override async Task<bool> AtualizarAsync(Livros livro) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE Livros
                SET
                    descricao = @descricao
                    , subtitulo = @subtitulo
                    , autorId = @autorId
                    , editoraId = @editoraId
                    , categoriaId = @categoriaId
                    , isbn = @isbn
                    , idioma = @idioma
                    , edicao = @edicao
                    , paginas = @paginas
                    , publicacao = @publicacao
                    , sinopse = @sinopse
                    , imagemCapa = @imagemCapa
                    , observacoes = @observacoes
                    , ativo = @ativo
                    , cadastro = @cadastro
                    , atualizacao = @atualizacao
                WHERE
                    id = @id
            ";

            var linhas = await connection.ExecuteAsync(sql, livro);
            return linhas > 0;
        }
    }
}