namespace ERP.Shared.Abstractions;

public interface ISmtpSender
{
    void SendEmail(string email, string body, string subject);
}
