namespace Library.Domain.DTOs.Auth {
    public class ResetPasswordRequest {
        public string Token { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
    }
}