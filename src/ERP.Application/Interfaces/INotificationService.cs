using ERP.Domain.Entities;
using ERP.Shared.Abstractions;
namespace ERP.Application.Interfaces;

public enum NotificationType
{
    Sms,
    Email,
    SystemNotification
}

public interface INotificationService : IAppService
{
    void SendNotification(User user, string message, NotificationType? type);
    void SendEmail(string emailToSend, string message, string subject);
}
