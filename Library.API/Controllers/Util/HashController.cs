using Library.Domain.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HashController : ControllerBase {
    private readonly IPasswordHasher _passwordHasher;

    public HashController(IPasswordHasher passwordHasher) {
        _passwordHasher = passwordHasher;
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult GerarHash([FromBody] string senha) {
        if (string.IsNullOrWhiteSpace(senha))
            return BadRequest("A senha é obrigatória.");

        var hash = _passwordHasher.Hash(senha);

        return Ok(new {
            senha,
            hash
        });
    }
}