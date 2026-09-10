using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class MultasRepository : RepositoryBase<Multas>, IMultasRepository {
        protected override string TableName => "Multas";
        protected override string PrimaryKeyColumn => "id";

        public MultasRepository(DatabaseConnection database) : base(database) { }

        public override async Task<int> CriarAsync(Multas multa) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                INSERT INTO Multas
                (
                    pessoaId
                    , emprestimoId
                    , motivo
                    , valor
                    , data
                    , pagamento
                    , status
                    , observacoes
                    , ativo
                    , cadastro
                    , atualizacao
                )
                VALUES
                (
                    @pessoaId
                    , @emprestimoId
                    , @motivo
                    , @valor
                    , @data
                    , @pagamento
                    , @status
                    , @observacoes
                    , @ativo
                    , @cadastro
                    , @atualizacao
                );

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            return await connection.ExecuteScalarAsync<int>(sql, multa);
        }

        public override async Task<bool> AtualizarAsync(Multas multa) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE Multas
                SET
                    pessoaId = @pessoaId
                    , emprestimoId = @emprestimoId
                    , motivo = @motivo
                    , valor = @valor
                    , data = @data
                    , pagamento = @pagamento
                    , status = @status
                    , observacoes = @observacoes
                    , ativo = @ativo
                    , cadastro = @cadastro
                    , atualizacao = @atualizacao
                WHERE
                    id = @id
            ";

            var linhas = await connection.ExecuteAsync(sql, multa);
            return linhas > 0;
        }
    }
}