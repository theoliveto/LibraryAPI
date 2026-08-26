using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories;

public class PessoasRepository : IPessoasRepository {
    private readonly DatabaseConnection _database;

    public PessoasRepository(DatabaseConnection database) {
        _database = database;
    }

    public async Task<IEnumerable<Pessoas>> ObterTodosAsync() {
        using var connection = _database.CreateConnection();

        const string sql = @"
            SELECT
                CD_PESSOA 	
                , DS_PESSOA 	
                , NR_CPF 		
                , DS_EMAIL 	
                , NR_TELEFONE 
                , DT_NASCIMENTO 	
                , DS_GENERO 		
                , DS_OBSERVACOES 	
                , NR_CEP 			
                , DS_ENDERECO 	
                , DS_CIDADE 		
                , DS_ESTADO 		
                , DS_BAIRRO 		
                , NR_NUMERO 		
                , DS_COMPLEMENTO 	
                , X_ATIVO 		
                , DT_CADASTRO 	
                , DT_ATUALIZACAO 	
            FROM 
                TBL_PESSOAS
            ORDER BY 
                DS_PESSOA
         ";

        return await connection.QueryAsync<Pessoas>(sql);
    }

    public async Task<Pessoas?> ObterPorIdAsync(int id) {
        using var connection = _database.CreateConnection();

        const string sql = @"
            SELECT
                CD_PESSOA 	
                , DS_PESSOA 	
                , NR_CPF 		
                , DS_EMAIL 	
                , NR_TELEFONE 
                , DT_NASCIMENTO 	
                , DS_GENERO 		
                , DS_OBSERVACOES 	
                , NR_CEP 			
                , DS_ENDERECO 	
                , DS_CIDADE 		
                , DS_ESTADO 		
                , DS_BAIRRO 		
                , NR_NUMERO 		
                , DS_COMPLEMENTO 	
                , X_ATIVO 		
                , DT_CADASTRO 	
                , DT_ATUALIZACAO 	
            FROM 
                TBL_PESSOAS
            WHERE 
                CD_PESSOA = @CD_PESSOA
        ";

        return await connection.QueryFirstOrDefaultAsync<Pessoas>(sql, new { CD_PESSOA = id });
    }

    public async Task<int> CriarAsync(Pessoas pessoa) {
        using var connection = _database.CreateConnection();

        const string sql = @"
            INSERT INTO TBL_PESSOAS
            (
                DS_PESSOA 	
                , NR_CPF 		
                , DS_EMAIL 	
                , NR_TELEFONE 
                , DT_NASCIMENTO 	
                , DS_GENERO 		
                , DS_OBSERVACOES 	
                , NR_CEP 			
                , DS_ENDERECO 	
                , DS_CIDADE 		
                , DS_ESTADO 		
                , DS_BAIRRO 		
                , NR_NUMERO 		
                , DS_COMPLEMENTO 	
                , X_ATIVO 		
                , DT_CADASTRO 	
                , DT_ATUALIZACAO 
            )
            VALUES
            (
                @DS_PESSOA 	
                , @NR_CPF 		
                , @DS_EMAIL 	
                , @NR_TELEFONE 
                , @DT_NASCIMENTO 	
                , @DS_GENERO 		
                , @DS_OBSERVACOES 	
                , @NR_CEP 			
                , @DS_ENDERECO 	
                , @DS_CIDADE 		
                , @DS_ESTADO 		
                , @DS_BAIRRO 		
                , @NR_NUMERO 		
                , @DS_COMPLEMENTO 	
                , @X_ATIVO 		
                , @DT_CADASTRO 	
                , @DT_ATUALIZACAO
            );
        ";

        return await connection.ExecuteScalarAsync<int>(sql, pessoa);
    }

    public async Task<bool> AtualizarAsync(Pessoas pessoa) {
        using var connection = _database.CreateConnection();

        const string sql = @"
            UPDATE 
                TBL_PESSOAS
            SET
                DS_PESSOA = @DS_PESSOA
                , NR_CPF = @NR_CPF
                , DS_EMAIL = @DS_EMAIL
                , NR_TELEFONE = @NR_TELEFONE
                , DT_NASCIMENTO = @DT_NASCIMENTO
                , DS_GENERO = @DS_GENERO
                , DS_OBSERVACOES = @DS_OBSERVACOES
                , NR_CEP = @NR_CEP  
                , DS_ENDERECO = @DS_ENDERECO
                , DS_CIDADE = @DS_CIDADE
                , DS_ESTADO = @DS_ESTADO
                , DS_BAIRRO = @DS_BAIRRO
                , NR_NUMERO = @NR_NUMERO
                , DS_COMPLEMENTO = @DS_COMPLEMENTO
                , X_ATIVO = @X_ATIVO
                , DT_ATUALIZACAO = @DT_ATUALIZACAO
            WHERE 
                CD_PESSOA = @CD_PESSOA
        ";

        var linhas = await connection.ExecuteAsync(sql, pessoa);

        return linhas > 0;
    }

    public async Task<bool> ExcluirAsync(int id) {
        using var connection = _database.CreateConnection();

        const string sql = @"
            DELETE FROM TBL_PESSOAS
            WHERE CD_PESSOA = @CD_PESSOA
        ";

        var linhas = await connection.ExecuteAsync(sql, new { CD_PESSOA = id });

        return linhas > 0;
    }

    public async Task<bool> VerificarCpfAsync(string cpf, int? cdPessoa) {
        using var connection = _database.CreateConnection();

        const string sql = @"
            SELECT *
            FROM TBL_PESSOAS
            WHERE NR_CPF = @NR_CPF
            AND (@CD_PESSOA IS NULL OR CD_PESSOA <> @CD_PESSOA)
        ";

        var linhas = await connection.QueryFirstOrDefaultAsync<Pessoas>(sql, new { NR_CPF = cpf, CD_PESSOA = cdPessoa });

        if (linhas == null) {
            return false;
        }

        return true;
    }

    public async Task<bool> VerificarEmailAsync(string email, int? cdPessoa) {
        using var connection = _database.CreateConnection();

        const string sql = @"
            SELECT *
            FROM TBL_PESSOAS
            WHERE DS_EMAIL = @DS_EMAIL
            AND (@CD_PESSOA IS NULL OR CD_PESSOA <> @CD_PESSOA)
        ";

        var linhas = await connection.QueryFirstOrDefaultAsync<Pessoas>(sql, new { DS_EMAIL = email, CD_PESSOA = cdPessoa });

        if (linhas == null) {
            return false;
        }

        return true;
    }
}