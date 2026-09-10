namespace Library.Domain.Entities {
    public class Categorias {
        public int id { get; set; }
        public string descricao { get; set; } = string.Empty;
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
        public DateTime cadastro { get; set; }
        public DateTime? atualizacao { get; set; }
    }
}