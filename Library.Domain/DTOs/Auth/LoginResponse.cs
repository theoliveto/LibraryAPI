namespace Library.Domain.DTOs.Auth { 
    public class LoginResponse {
        public string Token { get; set; } = string.Empty;
        public UsuarioAuthDto Usuario { get; set; } = new();
    }

    public class UsuarioAuthDto {
        public int id { get; set; }
        public string descricao { get; set; } = string.Empty;
        public string login { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public bool administrador { get; set; }
    }
}