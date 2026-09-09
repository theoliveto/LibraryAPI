using Library.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.DTOs.Autor;
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AutorController : ControllerBase {
        private readonly IAutorService _service;

        public AutorController(IAutorService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos() {
            var autores = await _service.ObterTodosAsync();

            return Ok(autores);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id) {
            var autor = await _service.ObterPorIdAsync(id);

            if (autor == null) {
                return NotFound(new { mensagem = "Autor(a) não encontrado(a)." });
            }

            return Ok(autor);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(PostRequest request) {
            try {
                var id = await _service.CriarAsync(request);

                var autor = await _service.ObterPorIdAsync(id);

                return CreatedAtAction(nameof(ObterPorId), new { id }, autor);

            } catch (ArgumentException ex) {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, PutRequest request) {
            try {
                var atualizado = await _service.AtualizarAsync(id, request);

                if (!atualizado) {
                    return NotFound(new { mensagem = "Autor(a) não encontrado(a)." });
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
                return NotFound(new { mensagem = "Autor(a) não encontrado(a)." });
            }

            return NoContent();
        }
    }
}