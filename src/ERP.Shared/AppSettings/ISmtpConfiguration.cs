using ERP.Shared.Abstractions;

public sealed class SmtpConfiguration : IAppOptions
{
    public static string ConfigSectionPath => "EmailConfiguration";

    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
}
