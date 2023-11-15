using FluentValidation;
using System;

namespace ERP.Application.Requests.UsersRequests
{
    public class ChangeProfileRequestValidator : AbstractValidator<ChangeProfileRequest>
    {
        public ChangeProfileRequestValidator()
        {
            RuleFor(req => req.UserId)
                .NotEmpty()
                .Must(BeAValidGuid)
                .WithMessage("UserId is not valid");

            RuleFor(req => req.ProfileId).NotEmpty();
        }

        private bool BeAValidGuid(Guid userId)
        {
            return userId != Guid.Empty;
        }
    }
}
