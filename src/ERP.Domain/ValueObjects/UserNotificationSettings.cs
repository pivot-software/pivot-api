using ERP.Domain.ValueObjects;
namespace ERP.Domain.ValueObjects
{
    public interface IUserNotificationSettings
    {
        bool SmsEnabled { get; }
        bool EmailEnabled { get; }
        bool SystemNotificationEnabled { get; }
        bool PushEnabled { get; }
    }
}

public class UserNotificationSettings : IUserNotificationSettings
{
    public UserNotificationSettings(bool smsEnabled, bool emailEnabled, bool systemNotificationEnabled, bool pushEnabled)
    {
        SmsEnabled = smsEnabled;
        EmailEnabled = emailEnabled;
        SystemNotificationEnabled = systemNotificationEnabled;
        PushEnabled = pushEnabled;
    }

    public bool SmsEnabled { get; private set; }
    public bool EmailEnabled { get; private set; }
    public bool SystemNotificationEnabled { get; private set; }
    public bool PushEnabled { get; private set; }
}
