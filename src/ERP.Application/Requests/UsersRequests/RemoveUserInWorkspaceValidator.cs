using FluentValidation;
using System;

namespace ERP.Application.Requests.UsersRequests
{
    public class RemoveUserInWorkspaceValidator : AbstractValidator<ChangeProfileRequest>
    {
        public RemoveUserInWorkspaceValidator()
        {
            RuleFor(req => req.UserId)
                .NotEmpty()
                .Must(BeAValidGuid)
                .WithMessage("UserId is not valid");
        }

        private bool BeAValidGuid(Guid userId)
        {
            return userId != Guid.Empty;
        }
    }
}
