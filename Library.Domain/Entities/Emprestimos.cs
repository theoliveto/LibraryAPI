namespace Library.Domain.Entities {
    public class Emprestimos {
        public int id { get; set; }
        public int pessoaId { get; set; }
        public int exemplarId { get; set; }
        public DateTime emprestimo { get; set; }
        public DateTime previsaoDevolucao { get; set; }
        public DateTime devolucao { get; set; }
        public string status { get; set; } = string.Empty;
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
        public DateTime cadastro { get; set; }
        public DateTime? atualizacao { get; set; }
    }
}