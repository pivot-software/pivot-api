using System.ComponentModel.DataAnnotations;
using ERP.Shared;
using ERP.Shared.Messages;

namespace ERP.Application.Requests.UsersRequest;

public class SignupRequest : BaseRequestWithValidation
{
    public SignupRequest(string email, string password, string name, Guid profileId)
    {
        Email = email;
        Password = password;
        Name = name;
        ProfileId = profileId;
    }

    [Required]
    [MaxLength(100)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; }

    [Required]
    [MinLength(4)]
    [DataType(DataType.Password)]
    public string Password { get; }

    [Required]
    [DataType(DataType.Text)]
    public string Name { get; }

    [Required]
    [DataType(DataType.Text)]
    public Guid ProfileId { get; }

    public override async Task ValidateAsync() =>
        ValidationResult = await LazyValidator.ValidateAsync<SignupRequestValidator>(this);

}
