using ERP.Domain.Entities;
using ERP.Shared.Abstractions;
namespace ERP.Application.Interfaces;

public interface INotificationService: IAppService
{
    void sendNotification(User user, string message);
}
