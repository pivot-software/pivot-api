using System.ComponentModel.DataAnnotations;
using ERP.Application.Requests.UsersRequests;
using ERP.Shared;
using ERP.Shared.Messages;

public class GetAllUsersPaginatedRequest : BaseRequestWithValidation
{
    // Construtor padrão sem parâmetros
    public GetAllUsersPaginatedRequest()
    {
    }

    public GetAllUsersPaginatedRequest(int page, string direction, int totalUsers, int pageSize)
    {
        Page = page;
        Direction = direction;
        PageSize = pageSize;
    }

    [Required] public int Page { get; set; }
    [Required] public string Direction { get; set; }
    [Required] public int PageSize { get; set; }

    public override async Task ValidateAsync() =>
        ValidationResult = await LazyValidator.ValidateAsync<GetAllUsersPaginatedRequestValidator>(this);
}