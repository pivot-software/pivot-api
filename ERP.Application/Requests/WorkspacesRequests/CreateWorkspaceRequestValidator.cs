using ERP.Shared.Extensions;
using FluentValidation;

namespace ERP.Application.Requests.WorkspaceRequests;

public class WorkspaceRequestValidator : AbstractValidator<CreateWorkspaceRequest>
{
    public WorkspaceRequestValidator()
    {
        RuleFor(req => req.BusinessName)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(req => req.BusinessColor)
            .NotEmpty()
            .IsValidColor()
            .MaximumLength(100);
    }
}
