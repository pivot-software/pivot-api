using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ERP.Application.Requests.AuthenticationRequests;
using ERP.Shared;
using ERP.Shared.Messages;

namespace ERP.Application.Requests.WorkspaceRequests;

public class CreateWorkspaceRequest : BaseRequestWithValidation
{
    public CreateWorkspaceRequest(string businessName, string businessLogo, string businessColor, string templateMode)
    {
        BusinessName = businessName;
        BusinessLogo = businessLogo;
        BusinessColor = businessColor;
        TemplateMode = templateMode;
    }

    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string BusinessName { get; }

    [DataType(DataType.Text)]
    public string BusinessLogo { get; }

    [MaxLength(50)]
    [DataType(DataType.Text)]
    public string BusinessColor { get; }

    [MaxLength(1)]
    [DataType(DataType.Text)]
    public string TemplateMode { get; }


    public override async Task ValidateAsync() =>
        ValidationResult = await LazyValidator.ValidateAsync<WorkspaceRequestValidator>(this);

}
