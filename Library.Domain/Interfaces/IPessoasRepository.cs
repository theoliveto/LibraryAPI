using Library.Domain.Entities;

namespace Library.Domain.Interfaces {
    public interface IPessoasRepository : IRepositoryBase<Pessoas> {
        Task<bool> VerificarCpfAsync(string cpf, int? cdPessoa);
        Task<bool> VerificarEmailAsync(string email, int? cdPessoa);
    }
}