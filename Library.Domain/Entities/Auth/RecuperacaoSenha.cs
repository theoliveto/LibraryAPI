namespace Library.Domain.Entities.Auth { 
    public class RecuperacaoSenha {
        public int id { get; set; }
        public int usuarioId { get; set; }
        public string token { get; set; } = string.Empty;
        public DateTime expiracao { get; set; }
        public DateTime? utilizacao { get; set; }
        public DateTime cadastro { get; set; }
    }
}