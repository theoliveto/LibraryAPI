using System.Text.Json.Serialization;

namespace Library.Domain.DTOs.Usuarios {
    public class PostRequest {
        [JsonPropertyName("dsUsuario")]
        public string DS_USUARIO { get; set; } = string.Empty;

        [JsonPropertyName("dsLogin")]
        public string DS_LOGIN { get; set; } = string.Empty;

        [JsonPropertyName("dsEmail")]
        public string DS_EMAIL { get; set; } = string.Empty;

        [JsonPropertyName("dsSenha")]
        public string DS_SENHA { get; set; } = string.Empty;

        [JsonPropertyName("dsObservacoes")]
        public string? DS_OBSERVACOES { get; set; }

        [JsonPropertyName("xAtivo")]
        public bool X_ATIVO { get; set; }

        [JsonPropertyName("xAdministrador")]
        public bool X_ADMINISTRADOR { get; set; }
    }
}