using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.DTOs.Autores;

namespace Library.API.Controllers {
    public class AutoresController : ApiControllerBase<Autores, PostRequest, PutRequest> {
        public AutoresController(IAutoresService service) : base(service) { }
    }
}