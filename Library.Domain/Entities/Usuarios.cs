namespace Library.Domain.Entities
{
    public class Usuarios
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public string Matricula { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}