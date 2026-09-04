using System.Text.Json.Serialization;

namespace Library.Domain.Entities {
    public class Empresas {
        [JsonPropertyName("cdEmpresa")]
        public int CD_EMPRESA { get; set; }

        [JsonPropertyName("dsRazaoSocial")]
        public string DS_RAZAO_SOCIAL { get; set; } = string.Empty;

        [JsonPropertyName("dsNomeFantasia")]
        public string DS_NOME_FANTASIA { get; set; } = string.Empty;

        [JsonPropertyName("nrCnpj")]
        public string NR_CNPJ { get; set; } = string.Empty;

        [JsonPropertyName("dsSigla")]
        public string? DS_SIGLA { get; set; }

        [JsonPropertyName("xAtivo")]
        public bool X_ATIVO { get; set; }

        [JsonPropertyName("dtCadastro")]
        public DateTime DT_CADASTRO { get; set; }

        [JsonPropertyName("dtAtualizacao")]
        public DateTime? DT_ATUALIZACAO { get; set; }
    }
}