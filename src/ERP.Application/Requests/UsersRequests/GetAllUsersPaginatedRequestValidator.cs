using FluentValidation;
using ERP.Shared.Extensions;


namespace ERP.Application.Requests.UsersRequests
{
    public class GetAllUsersPaginatedRequestValidator : AbstractValidator<GetAllUsersPaginatedRequest>
    {
        public GetAllUsersPaginatedRequestValidator()
        {
            RuleFor(req => req.Page)
                .NotEmpty();
        }
    }
}