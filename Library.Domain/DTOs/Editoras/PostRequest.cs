namespace Library.Domain.DTOs.Editoras {
    public class PostRequest {
        public string descricao { get; set; } = string.Empty;
        public string razaoSocial { get; set; } = string.Empty;
        public string cpfCnpj { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string celular { get; set; } = string.Empty;
        public string? telefone { get; set; }
        public string nacionalidade { get; set; } = string.Empty;
        public string siteOficial { get; set; } = string.Empty;
        public DateTime fundacao { get; set; }
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