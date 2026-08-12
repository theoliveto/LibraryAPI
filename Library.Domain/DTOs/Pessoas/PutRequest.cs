namespace Library.Domain.DTOs.Pessoas
{
    public class PutRequest
    {
        public string Nome { get; set; } = string.Empty;

        public string? CPF { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string? Email { get; set; }

        public string? Telefone { get; set; }
    }
}
