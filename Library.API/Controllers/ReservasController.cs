using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.DTOs.Reservas;

namespace Library.API.Controllers {
    public class ReservasController : ApiControllerBase<Reservas, PostRequest, PutRequest> {
        public ReservasController(IReservasService service) : base(service) { }
    }
}