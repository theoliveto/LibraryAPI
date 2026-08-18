namespace Library.Domain.Entities {
    public class Usuarios {
        public int CD_USUARIO { get; set; }
        public string DS_USUARIO { get; set; } = string.Empty;
        public string DS_LOGIN { get; set; } = string.Empty;
        public string DS_EMAIL { get; set; } = string.Empty;
        public string DS_SENHA { get; set; } = string.Empty;
        public string? DS_OBSERVACOES { get; set; }
        public bool X_ATIVO { get; set; }
        public bool X_ADMINISTRADOR { get; set; }
        public DateTime DT_CADASTRO { get; set; }
        public DateTime? DT_ATUALIZACAO { get; set; }
    }
}