using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.DTOs.Multas;

namespace Library.API.Controllers {
    public class MultasController : ApiControllerBase<Multas, PostRequest, PutRequest> {
        public MultasController(IMultasService service) : base(service) { }
    }
}