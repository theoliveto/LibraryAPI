using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories;

public class PessoasRepository : IPessoasRepository
{
    private readonly DatabaseConnection _database;

    public PessoasRepository(DatabaseConnection database)
    {
        _database = database;
    }

    public async Task<IEnumerable<Pessoas>> ObterTodosAsync()
    {
        using var connection = _database.CreateConnection();

        const string sql = @"
            SELECT
                id                  AS 'ID'
                , nome              AS 'Nome'
                , cpfCnpj           AS 'CPF/CNPJ'
                , nascimento        AS 'Dt. Nascimento'
                , email             AS 'E-mail'
                , genero            AS 'Gênero'
            FROM 
                Pessoas
            ORDER BY 
                nome
         ";

        return await connection.QueryAsync<Pessoas>(sql);
    }

    public async Task<Pessoas?> ObterPorIdAsync(int id)
    {
        using var connection = _database.CreateConnection();

        const string sql = @"
            SELECT
                id                  AS 'ID'
                , nome              AS 'Nome'
                , cpfCnpj           AS 'CPF/CNPJ'
                , nascimento        AS 'Dt. Nascimento'
                , email             AS 'E-mail'
                , genero            AS 'Gênero'
            FROM 
                Pessoas
            WHERE 
                id = @id
        ";

        return await connection.QueryFirstOrDefaultAsync<Pessoas>(sql, new { Id = id });
    }

    public async Task<int> CriarAsync(Pessoas pessoa)
    {
        using var connection = _database.CreateConnection();

        const string sql = @"
            INSERT INTO Pessoas
            (
                nome,
                cpfCnpj,
                nascimento,
                email,
                genero,
                cadastro
            )
            VALUES
            (
                @nome,
                @cpfCnpj,
                @nascimento,
                @email,
                @genero,
                @cadastro
            );

            SELECT CAST(SCOPE_IDENTITY() AS INT);
        ";

        return await connection.ExecuteScalarAsync<int>(sql, pessoa);
    }

    public async Task<bool> AtualizarAsync(Pessoas pessoa)
    {
        using var connection = _database.CreateConnection();

        const string sql = @"
            UPDATE 
                Pessoas
            SET
                nome = @nome,
                cpfCnpj = @cpfCnpj,
                nascimento = @nascimento,
                email = @email,
                genero = @genero,
                atualizacao = @atualizacao
            WHERE 
                id = @id
        ";

        var linhas = await connection.ExecuteAsync(sql, pessoa);

        return linhas > 0;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        using var connection = _database.CreateConnection();

        const string sql = @"
            DELETE FROM Pessoas
            WHERE id = @id
        ";

        var linhas = await connection.ExecuteAsync(sql, new { id = id });

        return linhas > 0;
    }
}