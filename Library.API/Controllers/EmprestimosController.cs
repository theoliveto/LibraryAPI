using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.DTOs.Emprestimos;

namespace Library.API.Controllers {
    public class EmprestimosController : ApiControllerBase<Emprestimos, PostRequest, PutRequest> {
        public EmprestimosController(IEmprestimosService service) : base(service) { }
    }
}