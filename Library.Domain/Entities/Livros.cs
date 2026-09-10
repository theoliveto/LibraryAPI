namespace Library.Domain.Entities {
    public class Livros {
        public int id { get; set; }
        public string descricao { get; set; } = string.Empty;
        public string? subtitulo { get; set; }
        public int autorId { get; set; }
        public int editoraId { get; set; }
        public int categoriaId { get; set; }
        public string isbn { get; set; } = string.Empty;
        public string idioma { get; set; } = string.Empty;
        public string edicao { get; set; } = string.Empty;
        public string paginas { get; set; } = string.Empty;
        public DateTime publicacao { get; set; }
        public string? sinopse { get; set; }
        public byte[]? imagemCapa { get; set; }
        public string? observacoes { get; set; }
        public bool ativo { get; set; }
        public DateTime cadastro { get; set; }
        public DateTime? atualizacao { get; set; }
    }
}