namespace Library.Domain.DTOs.Pessoas {
    public class PostRequest {
        public string descricao { get; set; } = string.Empty;
        public string? razaoSocial { get; set; }
        public int usuarioId { get; set; }
        public string cpfCnpj { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string celular { get; set; } = string.Empty;
        public string? telefone { get; set; }
        public DateTime nascimento { get; set; }
        public string genero { get; set; } = string.Empty;
        public string cep { get; set; } = string.Empty;
        public string endereco { get; set; } = string.Empty;
        public string cidade { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
        public string bairro { get; set; } = string.Empty;
        public string? numero { get; set; }
        public string? complemento { get; set; }
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
    }

    public class PutRequest : PostRequest {

    }
}