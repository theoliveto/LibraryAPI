namespace Library.Domain.DTOs.Autores {
    public class PostRequest {
        public string descricao { get; set; } = string.Empty;
        public DateTime nascimento { get; set; }
        public DateTime? falecimento { get; set; }
        public string nacionalidade { get; set; } = string.Empty;
        public string genero { get; set; } = string.Empty;
        public string? siteOficial { get; set; }
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
    }

    public class PutRequest : PostRequest {

    }
}