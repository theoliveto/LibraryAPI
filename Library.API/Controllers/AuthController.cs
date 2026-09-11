using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.DTOs.Auth;
using Library.Domain.Services.Auth;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) {
        try {
            var response = await _authService.LoginAsync(request);

            return Ok(response);
        } catch (UnauthorizedAccessException ex) {
            return Unauthorized(new {
                mensagem = ex.Message
            });
        } catch (ArgumentException ex) {
            return BadRequest(new {
                mensagem = ex.Message
            });
        }
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me() {
        var usuarioId = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(usuarioId, out var id)) {
            return Unauthorized();
        }

        var usuario = await _authService.ObterUsuarioAutenticadoAsync(id);

        if (usuario is null || !usuario.ativo) {
            return Unauthorized();
        }

        return Ok(new UsuarioAuthDto {
            id = usuario.id,
            descricao = usuario.descricao,
            login = usuario.login,
            email = usuario.email,
            perfil = usuario.perfil,
            administrador = usuario.administrador
        });
    }
}