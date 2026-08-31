using System.Text.Json.Serialization;

namespace Library.Domain.Entities { 
    public class RecuperacaoSenha {
        [JsonPropertyName("cdRecuperacao")]
        public int CD_RECUPERACAO { get; set; }

        [JsonPropertyName("cdUsuario")]
        public int CD_USUARIO { get; set; }

        [JsonPropertyName("dsToken")]
        public string DS_TOKEN { get; set; } = string.Empty;

        [JsonPropertyName("dtExpiracao")]
        public DateTime DT_EXPIRACAO { get; set; }

        [JsonPropertyName("dtUtilizacao")]
        public DateTime? DT_UTILIZACAO { get; set; }

        [JsonPropertyName("dtCadastro")]
        public DateTime DT_CADASTRO { get; set; }
    }
}