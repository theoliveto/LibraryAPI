namespace Library.Domain.Entities {
    public class Exemplares {
        public int id { get; set; }
        public int livroId { get; set; }
        public string patrimonio { get; set; } = string.Empty;
        public string? localizacao { get; set; }
        public string ean { get; set; } = string.Empty;
        public DateTime aquisicao { get; set; }
        public decimal valorAquisicao { get; set; }
        public string status { get; set; } = string.Empty;
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
        public DateTime cadastro { get; set; }
        public DateTime? atualizacao { get; set; }
    }
}