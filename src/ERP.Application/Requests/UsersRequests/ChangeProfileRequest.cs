using System.ComponentModel.DataAnnotations;
using ERP.Shared;
using ERP.Shared.Messages;
namespace ERP.Application.Requests.UsersRequests;

public class ChangeProfileRequest : BaseRequestWithValidation
{
    public ChangeProfileRequest(Guid userId, Guid profileId)
    {
        UserId = userId;
        ProfileId = profileId;
    }

    [Required]
    public Guid UserId { get; }

    [Required]
    public Guid ProfileId { get; }

    public override async Task ValidateAsync() =>
        ValidationResult = await LazyValidator.ValidateAsync<ChangeProfileRequestValidator>(this);
}
