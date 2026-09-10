using Library.Domain.Entities;
using Library.Domain.DTOs.Autores;

namespace Library.Domain.Services {
    public interface IAutoresService : IServiceBase<Autores, PostRequest, PutRequest> {

    }
}