using System.Net;
using System.Net.Mail;
using Library.Domain.Services;

namespace Library.Infrastructure.Services {
    public class EmailService : IEmailService {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration) {
            _configuration = configuration;
        }

        public async Task EnviarAsync(string destinatario, string assunto, string corpo) {
            var servidor = _configuration["Email:Smtp:Servidor"];
            var porta = _configuration.GetValue<int>("Email:Smtp:Porta");
            var usuario = _configuration["Email:Smtp:Usuario"];
            var senha = _configuration["Email:Smtp:Senha"];
            var remetente = _configuration["Email:Remetente"];

            if (string.IsNullOrWhiteSpace(servidor)) { 
                throw new InvalidOperationException("O servidor SMTP não foi configurado.");
            }

            if (string.IsNullOrWhiteSpace(usuario)) { 
                throw new InvalidOperationException("O usuário SMTP não foi configurado.");
            }

            if (string.IsNullOrWhiteSpace(senha)) { 
                throw new InvalidOperationException("A senha SMTP não foi configurada.");
            }

            if (string.IsNullOrWhiteSpace(remetente)) { 
                throw new InvalidOperationException("O remetente do e-mail não foi configurado.");
            }

            using var smtp = new SmtpClient(servidor, porta);

            smtp.EnableSsl = true;

            smtp.Credentials = new NetworkCredential(usuario, senha);

            using var mensagem = new MailMessage {
                From = new MailAddress(remetente),
                Subject = assunto,
                Body = corpo,
                IsBodyHtml = true
            };

            mensagem.To.Add(destinatario);

            await smtp.SendMailAsync(mensagem);
        }
    }
}