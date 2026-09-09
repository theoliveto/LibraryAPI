using Library.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.DTOs.Categorias;
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriasController : ControllerBase {
        private readonly ICategoriasService _service;

        public CategoriasController(ICategoriasService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos() {
            var categorias = await _service.ObterTodosAsync();

            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id) {
            var categorias = await _service.ObterPorIdAsync(id);

            if (categorias == null) {
                return NotFound(new { mensagem = "Categoria não encontrada." });
            }

            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(PostRequest request) {
            try {
                var id = await _service.CriarAsync(request);

                var categorias = await _service.ObterPorIdAsync(id);

                return CreatedAtAction(nameof(ObterPorId), new { id }, categorias);

            } catch (ArgumentException ex) {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, PutRequest request) {
            try {
                var atualizado = await _service.AtualizarAsync(id, request);

                if (!atualizado) {
                    return NotFound(new { mensagem = "Categoria não encontrada." });
                }

                return NoContent();

            } catch (ArgumentException ex) {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir(int id) {
            var excluido = await _service.ExcluirAsync(id);

            if (!excluido) {
                return NotFound(new { mensagem = "Categoria não encontrada." });
            }

            return NoContent();
        }
    }
}