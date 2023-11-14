using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using ERP.Application.Interfaces;
using ERP.Application.Requests.AuthenticationRequests;
using ERP.Application.Requests.UsersRequest;
using ERP.Application.Requests.UsersRequests;
using ERP.Application.Responses;
using ERP.Domain.DTO;
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
        IHashService hashService,
        INotificationService notificationService
        )
    {
        _dateTimeService = dateTimeService;
        _tokenClaimsService = tokenClaimsService;
        _repository = repository;
        _uow = uow;
        _hashService = hashService;
        _notificationService = notificationService;
    }

    #endregion

    #region Fields

    private readonly IDateTimeService _dateTimeService;
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IHashService _hashService;
    private readonly INotificationService _notificationService;

    #endregion


    #region Methods

    public async Task<Result<GetUserResponse[]>> GetUsersAsync()
    {
        var users = await _repository.GetAll();


        var userResponses = users.Select(user => new GetUserResponse(user.Id, user.Email,
            user.Username,
            user.Avatar,
            new ProfileDto(user.Profile.Id, user.Profile.ProfileName, user.Profile.Description),
            user.CreatedAt
        )).ToArray();

        return Result.Success(userResponses);


    }

    public async Task<Result<string>> AddUsersAsync(AddUsersInWorkspaceRequest request)
    {

        await request.ValidateAsync();

        if (!request.IsValid)
            return Result.Invalid(request.ValidationResult.AsErrors());

        // Carregar o conteúdo do arquivo HTML
        string templatePath = "../ERP.Application/Templates/InvitationTemplate.html";
        string htmlContent = System.IO.File.ReadAllText(templatePath);

        // Substituir placeholders no template
        htmlContent = htmlContent.Replace("{{ConviteLink}}", "https://seusite.com/aceitar-convite");

        _notificationService.SendEmail(request.Users[0], htmlContent, "Convite para o workspace");

        return Result.SuccessWithMessage("Convites enviados com sucesso");
    }

    #endregion

}
