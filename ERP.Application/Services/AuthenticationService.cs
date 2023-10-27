using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ardalis.Result;
using ERP.Application.Interfaces;
using ERP.Application.Requests.AuthenticationRequests;
using ERP.Application.Responses;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;
using ERP.Shared.Abstractions;


namespace ERP.Application.Services;

public class AuthenticationService : IAuthenticationService
{

    #region Constructor

    public AuthenticationService
        (
        IDateTimeService dateTimeService,
        ITokenClaimsService tokenClaimsService,
        IUserRepository repository
        )
    {
        _dateTimeService = dateTimeService;
        _tokenClaimsService = tokenClaimsService;
        _repository = repository;
    }

    #endregion

    #region Fields

    private readonly IDateTimeService _dateTimeService;
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly IUserRepository _repository;

    #endregion


    #region Methods

    public async Task<Result<TokenResponse>> AuthenticateAsync(LogInRequest request)
    {

        var user = await _repository.GetUserByEmail(request.Email);
        if (user == null)
            return Result.NotFound("A conta informada não existe.");


        // Gerando as regras (roles).
        var claims = GenerateClaims(user);

        // Gerando o token de acesso.

        var (accessToken, createdAt, expiresAt) = _tokenClaimsService.GenerateAccessToken(claims);

        // Gerando o token de atualização.
        var refreshToken = _tokenClaimsService.GenerateRefreshToken();

        return Result.Success(new TokenResponse(accessToken, createdAt, expiresAt, refreshToken));

    }

    private static Claim[] GenerateClaims(User user) => new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Sub, user.Username, ClaimValueTypes.String),
        new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString(), ClaimValueTypes.Email)
    };

    #endregion

}
