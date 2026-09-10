namespace Library.Domain.Entities {
    public class Autores {
        public int id { get; set; }
        public string descricao { get; set; } = string.Empty;
        public DateTime nascimento { get; set; }
        public DateTime? falecimento { get; set; }
        public string nacionalidade { get; set; } = string.Empty;
        public string genero { get; set; } = string.Empty;
        public string? siteOficial { get; set; }
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
        public DateTime cadastro { get; set; }
        public DateTime? atualizacao { get; set; }
    }
}