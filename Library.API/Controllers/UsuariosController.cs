using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.DTOs.Usuarios;

namespace Library.API.Controllers { 
    public class UsuariosController : ApiControllerBase<Usuarios, PostRequest, PutRequest> {
        public UsuariosController(IUsuariosService service) : base(service) { }
    }
}