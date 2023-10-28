using Ardalis.Result;
using ERP.Application.Requests.WorkspaceRequests;
using ERP.Application.Responses;
using ERP.Shared.Abstractions;
namespace ERP.Application.Interfaces;

public interface IWorkspaceServices : IAppService
{
    Task<Result<ConfigWorkspaceResponse>> CreateWorkspaceAsync(CreateWorkspaceRequest request);
}
