using System.Text.Json.Serialization;

namespace Library.Domain.DTOs.Pessoas {
    public class PostRequest {
        [JsonPropertyName("dsPessoa")]
        public string DS_PESSOA { get; set; } = string.Empty;

        [JsonPropertyName("nrCpf")]
        public string NR_CPF { get; set; } = string.Empty;

        [JsonPropertyName("dsEmail")]
        public string DS_EMAIL { get; set; } = string.Empty;

        [JsonPropertyName("nrTelefone")]
        public string NR_TELEFONE { get; set; } = string.Empty;

        [JsonPropertyName("dtNascimento")]
        public DateTime DT_NASCIMENTO { get; set; }

        [JsonPropertyName("dsGenero")]
        public string DS_GENERO { get; set; } = string.Empty;

        [JsonPropertyName("dsObservacoes")]
        public string? DS_OBSERVACOES { get; set; }

        [JsonPropertyName("nrCep")]
        public string NR_CEP { get; set; } = string.Empty;

        [JsonPropertyName("dsEndereco")]
        public string DS_ENDERECO { get; set; } = string.Empty;

        [JsonPropertyName("dsCidade")]
        public string DS_CIDADE { get; set; } = string.Empty;

        [JsonPropertyName("dsEstado")]
        public string DS_ESTADO { get; set; } = string.Empty;

        [JsonPropertyName("dsBairro")]
        public string DS_BAIRRO { get; set; } = string.Empty;

        [JsonPropertyName("nrNumero")]
        public string? NR_NUMERO { get; set; }

        [JsonPropertyName("dsComplemento")]
        public string? DS_COMPLEMENTO { get; set; }

        [JsonPropertyName("xAtivo")]
        public bool X_ATIVO { get; set; }
    }
}