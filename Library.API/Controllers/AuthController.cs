using Library.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.DTOs.Auth;
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
}