using System.Net.Mail;
using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using Microsoft.Extensions.Configuration;
namespace ERP.Application.Services;

public class NotificationService : INotificationService
{

    private readonly IConfiguration _configuration;


    public NotificationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendNotification(User user, string message, NotificationType? type)
    {
        this.SendEmail(user.Email, message, "subjectZ");
    }


    public void SendEmail(string email, string body, string subject)
    {

        string smtpServer = _configuration["EmailConfiguration:SmtpServer"] ?? throw new InvalidOperationException("SMTP server not configured");
        int port = _configuration.GetValue<int>("EmailConfiguration:Port");
        string username = _configuration["EmailConfiguration:Username"] ?? throw new InvalidOperationException("SMTP username not configured");
        string password = _configuration["EmailConfiguration:Password"] ?? throw new InvalidOperationException("SMTP password not configured");
        bool enableSsl = _configuration.GetValue<bool>("EmailConfiguration:EnableSsl");

        MailMessage mail = new MailMessage
        {
            From = new MailAddress("alvesjonatas99@gmail.com"),
            To = { email },
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        // Configurar o cliente SMTP
        SmtpClient smtpClient = new SmtpClient(smtpServer);

        smtpClient.Port = port;
        smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
        smtpClient.EnableSsl = enableSsl;

        // Enviar o e-mail
        smtpClient.Send(mail);
    }
}
