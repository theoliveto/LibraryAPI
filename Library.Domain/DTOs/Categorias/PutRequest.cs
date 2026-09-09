using System.Text.Json.Serialization;

namespace Library.Domain.DTOs.Categorias {
    public class PutRequest {
        [JsonPropertyName("dsCategoria")]
        public string DS_CATEGORIA { get; set; } = string.Empty;

        [JsonPropertyName("dsObservacoes")]
        public string? DS_OBSERVACOES { get; set; }

        [JsonPropertyName("xAtivo")]
        public bool X_ATIVO { get; set; }
    }
}