namespace Library.Domain.DTOs.Auth {
    public class LoginRequest {
        public string DS_LOGIN { get; set; } = string.Empty;
        public string DS_SENHA { get; set; } = string.Empty;
    }
}