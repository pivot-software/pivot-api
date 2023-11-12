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

        string? smtpServer = _configuration["EmailConfiguration:SmtpServer"];
        int port = _configuration.GetValue<int>("EmailConfiguration:Port");
        string? username = _configuration["EmailConfiguration:Username"];
        string? password = _configuration["EmailConfiguration:Password"];
        bool enableSsl = _configuration.GetValue<bool>("EmailConfiguration:EnableSsl");
        
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("alvesjonatas99@gmail.com");
        mail.To.Add(email);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        // Configurar o cliente SMTP
        SmtpClient smtpClient = new SmtpClient(smtpServer);
        smtpClient.Port = port;
        smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
        smtpClient.EnableSsl = enableSsl;

        // Enviar o e-mail
        smtpClient.Send(mail);
    }
}
