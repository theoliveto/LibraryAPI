namespace Library.Domain.DTOs.Multas {
    public class PostRequest {
        public int pessoaId { get; set; }
        public int emprestimoId { get; set; }
        public string motivo { get; set; } = string.Empty;
        public decimal valor { get; set; }
        public DateTime data { get; set; }
        public DateTime pagamento { get; set; }
        public string status { get; set; } = string.Empty;
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
    }

    public class PutRequest : PostRequest {

    }
}