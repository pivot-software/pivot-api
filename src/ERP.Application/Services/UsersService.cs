using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using ERP.Application.Interfaces;
using ERP.Application.Requests.AuthenticationRequests;
using ERP.Application.Requests.UsersRequest;
using ERP.Application.Requests.UsersRequests;
using ERP.Application.Responses;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;
using ERP.Shared.Abstractions;

namespace ERP.Application.Services;

public class UserService : IUsersService
{

    #region Constructor

    public UserService
        (
        IDateTimeService dateTimeService,
        ITokenClaimsService tokenClaimsService,
        IUserRepository repository,
        IUnitOfWork uow,
        IHashService hashService
        )
    {
        _dateTimeService = dateTimeService;
        _tokenClaimsService = tokenClaimsService;
        _repository = repository;
        _uow = uow;
        _hashService = hashService;
    }

    #endregion

    #region Fields

    private readonly IDateTimeService _dateTimeService;
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IHashService _hashService;

    #endregion


    #region Methods

    public async Task<Result<string>> AddUsersAsync(AddUsersInWorkspaceRequest request)
    {
        await request.ValidateAsync();
        if (!request.IsValid)
            return Result.Invalid(request.ValidationResult.AsErrors());

        try
        {
            foreach (var email_user in request.Users)
            {
                int index = email_user.IndexOf('@');
                string userName = email_user.Substring(0, index);
                var new_user = new User(email_user, userName, _hashService.Hash(email_user));
                _repository.Add(new_user);
                await _uow.CommitAsync();
            }

            return Result.SuccessWithMessage("Pedido de envio enviado");
        }
        catch (Exception e)
        {
            throw;
            return Result.Error();
        }
        return Result.Unauthorized();
    }

    #endregion

}
