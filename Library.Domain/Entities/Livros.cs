namespace Library.Domain.Entities
{
    public class Livros
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Edicao { get; set; } = string.Empty;
        public int AnoPublicacao { get; set; }
        public string Sinopse { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}