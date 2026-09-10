namespace Library.Domain.DTOs.Emprestimos {
    public class PostRequest {
        public int pessoaId { get; set; }
        public int exemplarId { get; set; }
        public DateTime emprestimo { get; set; }
        public DateTime previsaoDevolucao { get; set; }
        public DateTime devolucao { get; set; }
        public string status { get; set; } = string.Empty;
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
    }

    public class PutRequest : PostRequest {

    }
}