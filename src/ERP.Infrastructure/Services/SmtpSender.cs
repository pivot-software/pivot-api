using System.Net;
using System.Net.Mail;
using ERP.Shared.Abstractions;
using Microsoft.Extensions.Options;

public class SmtpSender : ISmtpSender
{
    private readonly SmtpClient _smtpClient;
    private readonly SmtpConfiguration _smtpConfiguration;

    public SmtpSender(IOptions<SmtpConfiguration> smtpConfigurationOptions)
    {
        _smtpConfiguration = smtpConfigurationOptions.Value;

        _smtpClient = new SmtpClient(_smtpConfiguration.SmtpServer)
        {
            Port = _smtpConfiguration.Port,
            Credentials = new NetworkCredential(_smtpConfiguration.Username, _smtpConfiguration.Password),
            EnableSsl = _smtpConfiguration.EnableSsl
        };
    }

    public void SendEmail(string email, string body, string subject)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("O endereço de e-mail não pode ser nulo ou vazio.", nameof(email));

        if (string.IsNullOrEmpty(body))
            throw new ArgumentException("O corpo do e-mail não pode ser nulo ou vazio.", nameof(body));

        if (string.IsNullOrEmpty(subject))
            throw new ArgumentException("O assunto do e-mail não pode ser nulo ou vazio.", nameof(subject));

        try
        {
            // Criar a mensagem de e-mail
            MailMessage mail = new MailMessage
            {
                From = new MailAddress("alvesjonatas99@gmail.com"),
                To = { email },
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            _smtpClient.Send(mail);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
            throw;
        }
    }
}
