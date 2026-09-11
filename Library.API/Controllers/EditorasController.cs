using Library.Domain.Entities;
using Library.Domain.Services;
using Library.Domain.DTOs.Editoras;

namespace Library.API.Controllers {
    public class EditorasController : ApiControllerBase<Editoras, PostRequest, PutRequest> {
        public EditorasController(IEditorasService service) : base(service) { }
    }
}