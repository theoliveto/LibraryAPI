using System.Text.Json.Serialization;

namespace Library.Domain.Entities {
    public class Autor {
        [JsonPropertyName("cdAutor")]
        public int CD_AUTOR { get; set; }

        [JsonPropertyName("dsAutor")]
        public string DS_AUTOR { get; set; } = string.Empty;

        [JsonPropertyName("dtNascimento")]
        public DateTime DT_NASCIMENTO { get; set; }

        [JsonPropertyName("dtFalecimento")]
        public DateTime? DT_FALECIMENTO { get; set; }

        [JsonPropertyName("dsPais")]
        public string DS_PAIS { get; set; } = string.Empty;

        [JsonPropertyName("dsGenero")]
        public string DS_GENERO { get; set; } = string.Empty;

        [JsonPropertyName("dsSiteOficial")]
        public string? DS_SITE_OFICIAL { get; set; }

        [JsonPropertyName("imFoto")]
        public byte[]? IM_FOTO { get; set; }

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