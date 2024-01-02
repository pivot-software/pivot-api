using ERP.Domain.DTO;
using ERP.Domain.Entities;
using PagedList;

namespace ERP.Application.Responses;

public class GetUserResponsePaginated
{
    public IPagedList<GetUserResponse> Data { get; set; }
    public bool HasLastPage { get; set; }
    public string Direction { get; set; }
    public int Page { get; set; }
    public int TotalUsers { get; set; }


    public GetUserResponsePaginated(IPagedList<GetUserResponse> data, bool hasLastPage, string direction, int page,
        int totalUsers)
    {
        Data = data;
        HasLastPage = hasLastPage;
        Direction = direction;
        Page = page;
        TotalUsers = totalUsers;
    }
}