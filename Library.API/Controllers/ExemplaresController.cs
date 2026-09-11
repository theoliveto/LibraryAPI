using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.DTOs.Exemplares;

namespace Library.API.Controllers {
    public class ExemplaresController : ApiControllerBase<Exemplares, PostRequest, PutRequest> {
        public ExemplaresController(IExemplaresService service) : base(service) { }
    }
}