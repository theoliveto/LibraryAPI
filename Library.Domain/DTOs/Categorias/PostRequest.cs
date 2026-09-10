namespace Library.Domain.DTOs.Categorias {
    public class PostRequest {
        public string descricao { get; set; } = string.Empty;
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
    }

    public class PutRequest : PostRequest {

    }
}