using FluentValidation;
using ERP.Shared.Extensions;

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
