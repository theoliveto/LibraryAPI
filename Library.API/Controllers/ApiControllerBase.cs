using Library.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase<TEntity, TPostRequest, TPutRequest> : ControllerBase
        where TEntity : class {

        protected readonly IServiceBase<TEntity, TPostRequest, TPutRequest> Service;

        protected ApiControllerBase(IServiceBase<TEntity, TPostRequest, TPutRequest> service) {
            Service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> ObterTodos() {
            var itens = await Service.ObterTodosAsync();
            return Ok(itens);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> ObterPorId(int id) {
            var item = await Service.ObterPorIdAsync(id);
            if (item is null) {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Criar(TPostRequest request) {
            var id = await Service.CriarAsync(request);
            return CreatedAtAction(nameof(ObterPorId), new { id }, null);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Atualizar(int id, TPutRequest request) {
            var atualizou = await Service.AtualizarAsync(id, request);
            if (!atualizou) {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Excluir(int id) {
            var excluiu = await Service.ExcluirAsync(id);
            if (!excluiu) {
                return NotFound();
            }
            return NoContent();
        }
    }
}