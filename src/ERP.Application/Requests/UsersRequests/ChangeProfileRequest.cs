using System.ComponentModel.DataAnnotations;
using ERP.Shared;
using ERP.Shared.Messages;
namespace ERP.Application.Requests.UsersRequests;

public class ChangeProfileRequest : BaseRequestWithValidation
{
    public ChangeProfileRequest(Guid userId, int profileId)
    {
        UserId = userId;
        ProfileId = profileId;
    }

    [Required]
    public Guid UserId { get; }

    [Required]
    public int ProfileId { get; }

    public override async Task ValidateAsync() =>
        ValidationResult = await LazyValidator.ValidateAsync<ChangeProfileRequestValidator>(this);
}
