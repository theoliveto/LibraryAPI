namespace Library.Domain.DTOs.Pessoas {
    public class PutRequest {
        public string DS_PESSOA { get; set; } = string.Empty;
        public string NR_CPF { get; set; } = string.Empty;
        public string DS_EMAIL { get; set; } = string.Empty;
        public string NR_TELEFONE { get; set; } = string.Empty;
        public DateTime DT_NASCIMENTO { get; set; }
        public string DS_GENERO { get; set; } = string.Empty;
        public string? DS_OBSERVACOES { get; set; }
        public string NR_CEP { get; set; } = string.Empty;
        public string DS_ENDERECO { get; set; } = string.Empty;
        public string DS_CIDADE { get; set; } = string.Empty;
        public string DS_ESTADO { get; set; } = string.Empty;
        public string DS_BAIRRO { get; set; } = string.Empty;
        public string? NR_NUMERO { get; set; }
        public string? DS_COMPLEMENTO { get; set; }
        public bool X_ATIVO { get; set; }
    }
}