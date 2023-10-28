using ERP.Application.Requests.UsersRequest;
using FluentValidation;
using ERP.Shared.Extensions;

namespace ERP.Application.Requests.UsersRequest;

public class SignupRequestValidator : AbstractValidator<SignupRequest>
{
    public SignupRequestValidator()
    {
        RuleFor(req => req.Email)
            .NotEmpty()
            .IsValidEmailAddress()
            .MaximumLength(100);

        RuleFor(req => req.Password)
            .NotEmpty()
            .MinimumLength(4);

        RuleFor(req => req.Name)
            .NotEmpty();
    }
}
