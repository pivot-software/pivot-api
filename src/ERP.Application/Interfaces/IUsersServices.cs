using System.Security.Claims;
using Ardalis.Result;
using ERP.Application.Requests.AuthenticationRequests;
using ERP.Application.Requests.UsersRequests;
using ERP.Application.Responses;
using ERP.Shared.Abstractions;
using PagedList;

namespace ERP.Application.Interfaces;

public interface IUsersService : IAppService
{
    Task<Result<string>> AddUsersAsync(AddUsersInWorkspaceRequest request);
   Task<Result<GetUserResponsePaginated>> GetUsersAsync(GetAllUsersPaginatedRequest request);
    Task<Result<String>> ChangeProfile(ChangeProfileRequest request);
    Task<Result<String>> RemoveUserInWorkspace(Guid request, ClaimsPrincipal user);
}
