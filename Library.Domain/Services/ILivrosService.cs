using Library.Domain.Entities;
using Library.Domain.DTOs.Livros;

namespace Library.Domain.Services {
    public interface ILivrosService : IServiceBase<Livros, PostRequest, PutRequest> {

    }
}