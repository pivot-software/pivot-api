
using FluentValidation;
using ERP.Shared.Extensions;


namespace ERP.Application.Requests.UsersRequests
{
    public class AddUsersInWorkspaceRequestValidator : AbstractValidator<AddUsersInWorkspaceRequest>
    {
        public AddUsersInWorkspaceRequestValidator()
        {
            RuleFor(req => req.Users)
                .NotEmpty().ForEach(userRule =>
                {
                    userRule.SetValidator(new UserValidator());
                });

            RuleFor(req => req.Users)
                .Must(BeUniqueEmails)
                .WithMessage("Os emails dos usuários devem ser únicos.");
        }

        private bool BeUniqueEmails(List<string> users)
        {
            return users.Distinct().Count() == users.Count;
        }
    }

    public class UserValidator : AbstractValidator<string>
    {
        public UserValidator()
        {
            RuleFor(user => user)
                .NotEmpty()
                .IsValidEmailAddress()
                .MaximumLength(100);
        }
    }
}
