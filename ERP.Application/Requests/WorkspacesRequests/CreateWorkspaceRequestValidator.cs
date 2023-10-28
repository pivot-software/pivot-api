using FluentValidation;

namespace ERP.Application.Requests.WorkspaceRequests;

public class WorkspaceRequestValidator : AbstractValidator<CreateWorkspaceRequest>
{
    public WorkspaceRequestValidator()
    {
        RuleFor(req => req.BusinessName)
            .NotEmpty()
            .MaximumLength(100);
    }
}
