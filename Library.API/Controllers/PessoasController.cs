using Library.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.DTOs.Pessoas;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase {
    private readonly IPessoasService _service;

    public PessoasController(IPessoasService service) {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos() {
        var pessoas = await _service.ObterTodosAsync();

        return Ok(pessoas);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorId(int id) {
        var pessoa = await _service.ObterPorIdAsync(id);

        if (pessoa == null) {
            return NotFound(new { mensagem = "Pessoa não encontrada." });
        }

        return Ok(pessoa);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(PostRequest request) {
        try {
            var id = await _service.CriarAsync(request);

            var pessoa = await _service.ObterPorIdAsync(id);

            return CreatedAtAction(nameof(ObterPorId), new { id }, pessoa);

        } catch(ArgumentException ex) {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, PutRequest request) {
        try {
            var atualizado = await _service.AtualizarAsync(id,request);

            if (!atualizado) {
                return NotFound(new { mensagem = "Pessoa não encontrada." });
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
            return NotFound(new { mensagem = "Pessoa não encontrada." });
        }

        return NoContent();
    }

    public async Task<bool> VerificarCpfAsync(string cpf, int? cdPessoa) {
        return await _service.VerificarCpfAsync(cpf, cdPessoa);
    }

    public async Task<bool> VerificarEmailAsync(string email, int? cdPessoa) {
        return await _service.VerificarEmailAsync(email, cdPessoa);
    }
}