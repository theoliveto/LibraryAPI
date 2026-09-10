using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Library.Domain.Services.Auth;

namespace Library.Infrastructure.Services.Auth; 
public class JwtService : IJwtService {
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration) {
        _configuration = configuration;
    }

    public string GerarToken(int cdUsuario, string nome, string login, string email, bool administrador) {
        var chave = _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(chave)) {
            throw new InvalidOperationException("A chave JWT não foi configurada.");
        }

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, cdUsuario.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, login),
            new(ClaimTypes.Name, nome),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, administrador ? "Administrador" : "Usuario")
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}