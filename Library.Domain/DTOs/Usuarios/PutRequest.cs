namespace Library.Domain.DTOs.Usuarios {
    public class PutRequest {
        public string DS_USUARIO { get; set; } = string.Empty;
        public string DS_LOGIN { get; set; } = string.Empty;
        public string DS_EMAIL { get; set; } = string.Empty;
        public string DS_SENHA { get; set; } = string.Empty;
        public string? DS_OBSERVACOES { get; set; }
        public bool X_ATIVO { get; set; }
        public bool X_ADMINISTRADOR { get; set; }
    }
}