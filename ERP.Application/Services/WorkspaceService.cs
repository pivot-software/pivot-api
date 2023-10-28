using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using ERP.Application.Interfaces;
using ERP.Application.Requests.WorkspaceRequests;
using ERP.Application.Responses;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;
using ERP.Shared.Abstractions;
using Microsoft.AspNetCore.Http;

namespace ERP.Application.Services;

public class WorkspaceService : IWorkspaceServices
{

    #region Constructor

    public WorkspaceService
        (
        IDateTimeService dateTimeService,
        ITokenClaimsService tokenClaimsService,
        IWorkspaceRepository repository,
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
    private readonly IWorkspaceRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IHashService _hashService;

    #endregion


    #region Methods

    public async Task<Result<ConfigWorkspaceResponse>> CreateWorkspaceAsync(CreateWorkspaceRequest request, ClaimsPrincipal user)
    {
        await request.ValidateAsync();
        if (!request.IsValid)
            return Result.Invalid(request.ValidationResult.AsErrors());

        // Você pode acessar as reivindicações do usuário, como o nome, ID, etc.
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var workspace = new Workspace();
        if (userId != null)
        {
            if (Guid.TryParse(userId, out Guid adminId))
            {
                workspace.AdminId = adminId;
                workspace.BusinessName = request.BusinessName;
                workspace.BusinessColor = request.BusinessColor;
                workspace.BusinessLogo = request.BusinessLogo;
                if (request.TemplateMode != null)
                {
                    workspace.TemplateMode = request.TemplateMode[0];
                }
            }
            else
            {
                return Result.Error("Usuário não encontrado");
            }
        }

        _repository.Add(workspace);
        await _uow.CommitAsync();

        return Result.Success();

        // var user = await _repository.GetUserByEmail(request.Email);
        //
        // if (user == null)
        // {
        //     return Result.NotFound("Nenhum usuário encontrado");
        // }
        //
        // if (_hashService.Compare(request.Password, user.Password))
        // {
        //     var claims = GenerateClaims(user);
        //
        //     var (accessToken, createdAt, expiresAt) = _tokenClaimsService.GenerateAccessToken(claims);
        //
        //     var refreshToken = _tokenClaimsService.GenerateRefreshToken();
        //
        //     user.AddToken(accessToken, refreshToken, expiresAt);
        //     _repository.Update(user);
        //     await _uow.CommitAsync();
        //
        //     return Result.Success(new TokenResponse(accessToken, createdAt, expiresAt, refreshToken));
        // }

        // return Result.Unauthorized();
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
