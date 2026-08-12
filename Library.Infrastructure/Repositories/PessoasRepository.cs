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
                ID                  AS Id
                , NOME              AS Nome
                , CPF               AS CPF
                , DATA_NASCIMENTO   AS DataNascimento
                , EMAIL             AS Email
                , TELEFONE          AS Telefone
                , DATA_CADASTRO     AS DataCadastro
            FROM 
                PESSOAS
            ORDER BY 
                NOME
         ";

        return await connection.QueryAsync<Pessoas>(sql);
    }

    public async Task<Pessoas?> ObterPorIdAsync(int id)
    {
        using var connection = _database.CreateConnection();

        const string sql = @"
            SELECT
                ID AS Id,
                NOME AS Nome,
                CPF AS CPF,
                DATA_NASCIMENTO AS DataNascimento,
                EMAIL AS Email,
                TELEFONE AS Telefone,
                DATA_CADASTRO AS DataCadastro
            FROM PESSOAS
            WHERE ID = @Id
        ";

        return await connection.QueryFirstOrDefaultAsync<Pessoas>(sql, new { Id = id });
    }

    public async Task<int> CriarAsync(Pessoas pessoa)
    {
        using var connection = _database.CreateConnection();

        const string sql = @"
            INSERT INTO PESSOAS
            (
                NOME,
                CPF,
                DATA_NASCIMENTO,
                EMAIL,
                TELEFONE,
                DATA_CADASTRO
            )
            VALUES
            (
                @Nome,
                @CPF,
                @DataNascimento,
                @Email,
                @Telefone,
                @DataCadastro
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
                PESSOAS
            SET
                NOME = @Nome,
                CPF = @CPF,
                DATA_NASCIMENTO = @DataNascimento,
                EMAIL = @Email,
                TELEFONE = @Telefone
            WHERE 
                ID = @Id
        ";

        var linhas = await connection.ExecuteAsync(sql, pessoa);

        return linhas > 0;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        using var connection = _database.CreateConnection();

        const string sql = @"
            DELETE FROM PESSOAS
            WHERE ID = @Id
        ";

        var linhas = await connection.ExecuteAsync(sql, new { Id = id });

        return linhas > 0;
    }
}