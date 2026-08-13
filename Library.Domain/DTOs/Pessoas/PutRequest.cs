namespace Library.Domain.DTOs.Pessoas
{
    public class PutRequest
    {
        public string nome { get; set; } = string.Empty;
        public string? cpfCnpj { get; set; }
        public DateTime? nascimento { get; set; }
        public string? email { get; set; }
        public string? genero { get; set; }
    }
}
