using GPD.Backend.Domain.Services.Contracts;
using System.Net;
using System.Net.Mail;

namespace GPD.Backend.Domain.Services.Implementations
{
    //TODO - precisa melhorar e colocar mais opções para E-mail (anexos, cópia de destinatários...)
    public class EmailService : IEmailService
    {
        private readonly EnvironmentService environmentService;

        public EmailService(EnvironmentService environmentService) => this.environmentService = environmentService;

        public void EnviarComCopiaOcultaRemetente(string assunto, string remetente, string destinatario, string body)
        {
            using var smtp = new SmtpClient(environmentService.Smtp)
            {
                EnableSsl = environmentService.EnableSsl,
                Port = environmentService.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(environmentService.Login, environmentService.Senha)
            };

            var mail = new MailMessage
            {
                From = new MailAddress(remetente)
            };

            mail.To.Add(destinatario);
            mail.Bcc.Add(remetente);
            mail.Subject = assunto;
            mail.Body = body;
            smtp.Send(mail);
        }
    }
}
