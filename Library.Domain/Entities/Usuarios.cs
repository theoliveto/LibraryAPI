namespace Library.Domain.Entities { 
    public class Usuarios {
        public int id { get; set; }
        public string descricao { get; set; } = string.Empty;
        public string login { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string senha { get; set; } = string.Empty;
        public string perfil { get; set; } = string.Empty;
        public byte[]? avatar { get; set; }
        public bool administrador { get; set; }
        public int tentativasLogin { get; set; }
        public DateTime? bloqueioLogin { get; set; }
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
        public DateTime cadastro { get; set; }
        public DateTime? atualizacao { get; set; }
    }
}