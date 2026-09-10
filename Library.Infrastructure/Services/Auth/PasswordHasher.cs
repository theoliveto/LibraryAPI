using Library.Domain.Interfaces.Auth;

namespace Library.Infrastructure.Services.Auth; 
public class PasswordHasher : IPasswordHasher {
    public string Hash(string senha) {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public bool Verify(string senha, string hash) {
        return BCrypt.Net.BCrypt.Verify(senha, hash);
    }
}