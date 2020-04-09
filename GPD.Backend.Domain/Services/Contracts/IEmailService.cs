namespace GPD.Backend.Domain.Services.Contracts
{
    public interface IEmailService
    {
        void EnviarComCopiaOcultaRemetente(string assunto, string remetente, string destinatario, string body);
    }
}
