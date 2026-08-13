namespace Library.Domain.Entities
{
    public class Pessoas
    {
        public int id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string? cpfCnpj { get; set; }
        public DateTime? nascimento { get; set; }
        public string? email { get; set; }
        public string? genero { get; set; }
        public DateTime cadastro { get; set; }
        public DateTime? atualizacao { get; set; }
    }
}