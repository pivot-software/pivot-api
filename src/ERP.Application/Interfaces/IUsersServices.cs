using Ardalis.Result;
using ERP.Application.Requests.AuthenticationRequests;
using ERP.Application.Requests.UsersRequests;
using ERP.Application.Responses;
using ERP.Shared.Abstractions;

namespace ERP.Application.Interfaces;

public interface IUsersService : IAppService
{
    Task<Result<string>> AddUsersAsync(AddUsersInWorkspaceRequest request);
}
