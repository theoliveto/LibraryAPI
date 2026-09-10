using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories {
    public class ReservasRepository : RepositoryBase<Reservas>, IReservasRepository {
        protected override string TableName => "Reservas";
        protected override string PrimaryKeyColumn => "id";

        public ReservasRepository(DatabaseConnection database) : base(database) { }

        public override async Task<int> CriarAsync(Reservas reserva) {
            using var connection = Database.CreateConnection();
            const string sql = @"
                INSERT INTO Reservas
                (
                    pessoaId
                    , exemplarId
                    , reserva
                    , prazo
                    , validade
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
                    , @reserva
                    , @prazo
                    , @validade
                    , @status
                    , @observacoes
                    , @ativo
                    , @cadastro
                    , @atualizacao
                );
                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";
            return await connection.ExecuteScalarAsync<int>(sql, reserva);
        }

        public override async Task<bool> AtualizarAsync(Reservas reserva) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE 
                    Reservas
                SET
                    pessoaId = @pessoaId
                    , exemplarId = @exemplarId
                    , reserva = @reserva
                    , prazo = @prazo
                    , validade = @validade
                    , status = @status
                    , observacoes = @observacoes
                    , ativo = @ativo
                    , cadastro = @cadastro
                    , atualizacao = @atualizacao
                WHERE 
                    id = @id
            ";

            var linhas = await connection.ExecuteAsync(sql, reserva);

            return linhas > 0;
        }
    }
}