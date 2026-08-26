namespace Library.Domain.DTOs.Auth; 
public class LoginResponse {
    public string Token { get; set; } = string.Empty;

    public UsuarioAuthDto Usuario { get; set; } = new();
}

public class UsuarioAuthDto {
    public int CD_USUARIO { get; set; }
    public string DS_USUARIO { get; set; } = string.Empty;
    public string DS_LOGIN { get; set; } = string.Empty;
    public string DS_EMAIL { get; set; } = string.Empty;
    public bool X_ADMINISTRADOR { get; set; }
}