using System.Text.Json.Serialization;

namespace Library.Domain.Entities; 
public class Usuarios {
    [JsonPropertyName("cdUsuario")]
    public int CD_USUARIO { get; set; }

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

    [JsonPropertyName("dtCadastro")]
    public DateTime DT_CADASTRO { get; set; }

    [JsonPropertyName("dtAtualizacao")]
    public DateTime? DT_ATUALIZACAO { get; set; }
}