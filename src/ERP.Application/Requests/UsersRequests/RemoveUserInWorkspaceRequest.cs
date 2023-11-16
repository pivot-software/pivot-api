using System.ComponentModel.DataAnnotations;
using ERP.Shared;
using ERP.Shared.Messages;
namespace ERP.Application.Requests.UsersRequests;

public class RemoveUserInWorkspaceRequest : BaseRequestWithValidation
{
    public RemoveUserInWorkspaceRequest(Guid userId)
    {
        UserId = userId;
    }

    [Required]
    public Guid UserId { get; }


    public override async Task ValidateAsync() =>
        ValidationResult = await LazyValidator.ValidateAsync<RemoveUserInWorkspaceValidator>(this);
}
