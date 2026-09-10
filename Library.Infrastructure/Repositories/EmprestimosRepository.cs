using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class EmprestimosRepository : RepositoryBase<Emprestimos>, IEmprestimosRepository {
        protected override string TableName => "Emprestimos";
        protected override string PrimaryKeyColumn => "id";

        public EmprestimosRepository(DatabaseConnection database) : base(database) { }

        public override async Task<int> CriarAsync(Emprestimos emprestimo) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                INSERT INTO Emprestimos
                (
                    pessoaId
                    , exemplarId
                    , emprestimo
                    , previsaoDevolucao
                    , devolucao
                    , status
                    , observacoes
                    , ativo
                    , cadastro
                    , atualizacao
                )
                VALUES
                (
                    @pessoaId
                    , @exemplarId
                    , @emprestimo
                    , @previsaoDevolucao
                    , @devolucao
                    , @status
                    , @observacoes
                    , @ativo
                    , @cadastro
                    , @atualizacao
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, emprestimo);
        }

        public override async Task<bool> AtualizarAsync(Emprestimos emprestimo) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE Emprestimos
                SET
                    pessoaId = @pessoaId
                    , exemplarId = @exemplarId
                    , emprestimo = @emprestimo
                    , previsaoDevolucao = @previsaoDevolucao
                    , devolucao = @devolucao
                    , status = @status
                    , observacoes = @observacoes
                    , ativo = @ativo
                    , cadastro = @cadastro
                    , atualizacao = @atualizacao
                WHERE
                    id = @id
            ";

            var linhas = await connection.ExecuteAsync(sql, emprestimo);
            return linhas > 0;
        }
    }
}