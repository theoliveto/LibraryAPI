using Library.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        if (usuario is null || !usuario.X_ATIVO) {
            return Unauthorized();
        }

        return Ok(new UsuarioAuthDto {
            CD_USUARIO = usuario.CD_USUARIO,
            DS_USUARIO = usuario.DS_USUARIO,
            DS_LOGIN = usuario.DS_LOGIN,
            DS_EMAIL = usuario.DS_EMAIL,
            X_ADMINISTRADOR = usuario.X_ADMINISTRADOR
        });
    }
}