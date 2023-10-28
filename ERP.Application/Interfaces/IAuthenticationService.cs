using Ardalis.Result;
using ERP.Application.Requests.AuthenticationRequests;
using ERP.Application.Requests.UsersRequest;
using ERP.Application.Responses;
using ERP.Domain.Entities;
using ERP.Shared.Abstractions;

namespace ERP.Application.Interfaces;

public interface IAuthenticationService : IAppService
{
    Task<Result<TokenResponse>> AuthenticateAsync(LogInRequest request);
    Task<Result<User>> CreateUser(SignupRequest request);
}
