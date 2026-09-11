using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.DTOs.Livros;

namespace Library.API.Controllers {
    public class LivrosController : ApiControllerBase<Livros, PostRequest, PutRequest> {
        public LivrosController(ILivrosService service) : base(service) { }
    }
}