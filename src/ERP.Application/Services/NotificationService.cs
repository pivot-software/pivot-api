using System.Net.Mail;
using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using ERP.Shared.Abstractions;
using Microsoft.Extensions.Configuration;
namespace ERP.Application.Services;

public class NotificationService : INotificationService
{

    private readonly IConfiguration _configuration;
    private readonly ISmtpSender _smtpClient;

    public NotificationService(IConfiguration configuration, ISmtpSender smtpClient)
    {
        _configuration = configuration;
        _smtpClient = smtpClient;
    }

    public void SendNotification(User user, string message, NotificationType? type)
    {
        this.SendEmail(user.Email, message, "subjectZ");
    }

    public void SendEmail(string email, string body, string subject)
    {
        _smtpClient.SendEmail(email, body, subject);
    }
}
