namespace Library.Domain.Services.Auth {
    public interface IEmailService {
        Task EnviarAsync(string destinatario, string assunto, string corpo);
    }
}