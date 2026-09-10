using Library.Domain.Entities;
using Library.Domain.DTOs.Pessoas;

namespace Library.Domain.Services {
    public interface IPessoasService : IServiceBase<Pessoas, PostRequest, PutRequest> {
        Task<bool> VerificarCpfAsync(string cpf, int? cdPessoa);
        Task<bool> VerificarEmailAsync(string email, int? cdPessoa);
    }
}