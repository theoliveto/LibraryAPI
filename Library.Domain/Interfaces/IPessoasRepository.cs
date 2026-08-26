using Library.Domain.Entities;

namespace Library.Domain.Interfaces {
    public interface IPessoasRepository {
        Task<IEnumerable<Pessoas>> ObterTodosAsync();

        Task<Pessoas?> ObterPorIdAsync(int id);

        Task<int> CriarAsync(Pessoas pessoa);

        Task<bool> AtualizarAsync(Pessoas pessoa);

        Task<bool> ExcluirAsync(int id);

        Task<bool> VerificarCpfAsync(string cpf, int? cdPessoa);

        Task<bool> VerificarEmailAsync(string email, int? cdPessoa);
    }
}