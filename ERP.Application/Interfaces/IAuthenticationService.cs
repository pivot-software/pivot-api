using Ardalis.Result;
using ERP.Application.Requests.AuthenticationRequests;
using ERP.Application.Services;
using ERP.Shared.Abstractions;
using ERP.Shared.Messages;

namespace ERP.Application.Interfaces;

public interface IAuthenticationService: IAppService
{
    Task<Result<string>> AuthenticateAsync(LogInRequest request);
}