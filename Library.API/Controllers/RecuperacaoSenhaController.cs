using Library.Domain.DTOs.Auth;
using Library.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers {
    [ApiController]
    [Route("api/auth")]
    public class RecuperacaoSenhaController : ControllerBase {
        private readonly IRecuperacaoSenhaService _passwordResetService;

        public RecuperacaoSenhaController(IRecuperacaoSenhaService passwordResetService) {
            _passwordResetService = passwordResetService;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> SolicitarRecuperacao([FromBody] ForgotPasswordRequest request) {
            try {
                await _passwordResetService.SolicitarRecuperacaoAsync(
                    request.Email
                );

                return Ok(new {
                    mensagem = "Se o e-mail estiver cadastrado, você receberá as instruções para redefinir sua senha."
                });
            } catch (ArgumentException ex) {
                return BadRequest(new {
                    mensagem = ex.Message
                });
            }
        }

        [HttpGet("reset-password/{token}")]
        public async Task<IActionResult> ValidarToken(string token) {
            var valido = await _passwordResetService.ValidarTokenAsync(token);

            if (!valido) {
                return BadRequest(new { 
                    valido = false,
                    mensagem = "O token é inválido ou expirou." 
                });
            }

            return Ok(new {
                valido = true,
                mensagem = "Token válido."
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> RedefinirSenha([FromBody] ResetPasswordRequest request) {
            try {
                var sucesso =
                    await _passwordResetService.RedefinirSenhaAsync(
                        request.Token,
                        request.NovaSenha
                    );

                if (!sucesso) {
                    return BadRequest(new {
                        mensagem = "O token é inválido ou expirou."
                    });
                }

                return Ok(new {
                    mensagem = "Senha redefinida com sucesso."
                });
            } catch (ArgumentException ex) {
                return BadRequest(new {
                    mensagem = ex.Message
                });
            }
        }
    }
}