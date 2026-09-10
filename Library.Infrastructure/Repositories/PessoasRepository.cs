using Dapper;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories { 
    public class PessoasRepository : RepositoryBase<Pessoas>, IPessoasRepository {
        protected override string TableName => "Pessoas";
        protected override string PrimaryKeyColumn => "id";

        public PessoasRepository(DatabaseConnection database) : base(database) { }

        public override async Task<int> CriarAsync(Pessoas pessoa) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                INSERT INTO Pessoas
                (
                    descricao 	
                    , razaoSocial 		
                    , usuarioId 	
                    , cpfCnpj 
                    , email 	
                    , celular 		
                    , telefone 	
                    , nascimento 			
                    , genero 	
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
                    , @usuarioId 	
                    , @cpfCnpj 
                    , @email 	
                    , @celular 		
                    , @telefone 	
                    , @nascimento 			
                    , @genero 	
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
            ";

            return await connection.ExecuteScalarAsync<int>(sql, pessoa);
        }

        public override async Task<bool> AtualizarAsync(Pessoas pessoa) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                UPDATE 
                    Pessoas
                SET
                    descricao = @descricao
                    , razaoSocial = @razaoSocial
                    , usuarioId = @usuarioId 	
                    , cpfCnpj = @cpfCnpj
                    , email = @email 	
                    , celular = @celular		
                    , telefone = @telefone 	
                    , nascimento = @nascimento 			
                    , genero = @genero
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

            var linhas = await connection.ExecuteAsync(sql, pessoa);

            return linhas > 0;
        }

        public async Task<bool> VerificarCpfAsync(string cpf, int? cdPessoa) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                SELECT *
                FROM Pessoas
                WHERE cpfCnpj = @cpfCnpj
                AND (@id IS NULL OR id <> @id)
            ";

            var linhas = await connection.QueryFirstOrDefaultAsync<Pessoas>(sql, new { cpfCnpj = cpf, id = cdPessoa });

            if (linhas == null) {
                return false;
            }

            return true;
        }

        public async Task<bool> VerificarEmailAsync(string email, int? cdPessoa) {
            using var connection = Database.CreateConnection();

            const string sql = @"
                SELECT *
                FROM Pessoas
                WHERE email = @email
                AND (@id IS NULL OR id <> @id)
            ";

            var linhas = await connection.QueryFirstOrDefaultAsync<Pessoas>(sql, new { email = email, id = cdPessoa });

            if (linhas == null) {
                return false;
            }

            return true;
        }
    }
}