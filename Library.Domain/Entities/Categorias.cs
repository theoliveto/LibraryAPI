using System.Text.Json.Serialization;

namespace Library.Domain.Entities {
    public class Categorias {
        [JsonPropertyName("cdCategoria")]
        public int CD_CATEGORIA { get; set; }

        [JsonPropertyName("dsCategoria")]
        public string DS_CATEGORIA { get; set; } = string.Empty;

        [JsonPropertyName("dsObservacoes")]
        public string? DS_OBSERVACOES { get; set; }

        [JsonPropertyName("xAtivo")]
        public bool X_ATIVO { get; set; }

        [JsonPropertyName("dtCadastro")]
        public DateTime DT_CADASTRO { get; set; }

        [JsonPropertyName("dtAtualizacao")]
        public DateTime? DT_ATUALIZACAO { get; set; }
    }
}