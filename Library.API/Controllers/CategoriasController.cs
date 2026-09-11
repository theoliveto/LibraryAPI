using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.DTOs.Categorias;

namespace Library.API.Controllers {
    public class CategoriasController : ApiControllerBase<Categorias, PostRequest, PutRequest> {
        public CategoriasController(ICategoriasService service) : base(service) { }
    }
}