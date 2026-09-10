namespace Library.Domain.DTOs.Reservas {
    public class PostRequest {
        public int pessoaId { get; set; }
        public int exemplarId { get; set; }
        public DateTime reserva { get; set; }
        public string prazo { get; set; } = string.Empty;
        public DateTime validade { get; set; }
        public string status { get; set; } = string.Empty;
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
    }

    public class PutRequest : PostRequest {

    }
}