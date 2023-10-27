using Ardalis.Result;
using ERP.Application.Interfaces;
using ERP.Application.Requests.AuthenticationRequests;
using ERP.Domain.Repositories;
using ERP.Shared.Messages;

namespace ERP.Application.Services;

public class AuthenticationService : IAuthenticationService
{

    #region Constructor

    public AuthenticationService
        (
        IUserRepository repository
        )
    {

        _repository = repository;
    }

    #endregion

    #region Fields

    private readonly IUserRepository _repository;

    #endregion

    #region Methods

    public async Task<Result<string>> AuthenticateAsync(LogInRequest request)
    {
        var user = await _repository.GetUserByEmail(request.Email);

        if (user != null)
            return user.Email;

        return Result.Error("O e-mail ou senha está incorreta.");

    }

    #endregion

}
