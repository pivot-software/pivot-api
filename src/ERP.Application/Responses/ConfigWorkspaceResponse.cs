using ERP.Shared.Messages;
namespace ERP.Application.Responses;

public sealed class ConfigWorkspaceResponse: IResponse
{
    public string BusinessName { get; set; }
    public string BusinessLogo { get; set; }
    public string BusinessColor { get; set; }
    public string TemplateMode { get; set; }
}
