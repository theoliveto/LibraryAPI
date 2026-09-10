namespace Library.Domain.DTOs.Auth { 
    public class LoginRequest {
        public string dsLogin { get; set; } = string.Empty;
        public string dsSenha { get; set; } = string.Empty;
    }
}