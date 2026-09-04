using System.Text.Json.Serialization;

namespace Library.Domain.Entities {
    public class UsuariosEmpresas {
        [JsonPropertyName("cdId")]
        public int CD_ID { get; set; }

        [JsonPropertyName("cdUsuario")]
        public int CD_USUARIO { get; set; }

        [JsonPropertyName("cdEmpresa")]
        public int CD_EMPRESA { get; set; }

        [JsonPropertyName("xAtivo")]
        public bool X_ATIVO { get; set; }

        [JsonPropertyName("dtCadastro")]
        public DateTime DT_CADASTRO { get; set; }

        [JsonPropertyName("dtAtualizacao")]
        public DateTime? DT_ATUALIZACAO { get; set; }
    }
}