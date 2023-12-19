using System.Security.Claims;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using ERP.Application.Interfaces;
using ERP.Application.Requests.UsersRequests;
using ERP.Application.Responses;
using ERP.Domain.DTO;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;
using ERP.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using PagedList;

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
        INotificationService notificationService,
        IProfileRepository repositoryProfile
    )
    {
        _dateTimeService = dateTimeService;
        _tokenClaimsService = tokenClaimsService;
        _repository = repository;
        _uow = uow;
        _hashService = hashService;
        _notificationService = notificationService;
        _repositoryProfile = repositoryProfile;
    }

    #endregion

    #region Fields

    private readonly IDateTimeService _dateTimeService;
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly IUserRepository _repository;
    private readonly IProfileRepository _repositoryProfile;
    private readonly IUnitOfWork _uow;
    private readonly IHashService _hashService;
    private readonly INotificationService _notificationService;

    #endregion


    #region Methods

    public async Task<Result<String>> RemoveUserInWorkspace(Guid request, ClaimsPrincipal user)
    {
        try
        {
            var userObject = await _repository.GetUserById(request);

            if (userObject == null)
                return Result.NotFound("Usuário não encontrado");

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                if (Guid.TryParse(userId, out Guid adminId))
                {
                    userObject.DeletedBy = adminId;
                    _repository.Update(userObject);
                }
                else
                {
                    return Result.Error("Nenhum token informado");
                }
            }

            try
            {
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                return Result.Error($"Ocorreu um erro durante a confirmação das alterações: {ex.Message}");
            }

            return Result.Success("Usuário removido com sucesso");
        }
        catch (Exception ex)
        {
            return Result.Error($"Ocorreu um erro durante a validação: {ex.Message}");
        }
    }

    public async Task<Result<String>> ChangeProfile(ChangeProfileRequest request)
    {
        try
        {
            await request.ValidateAsync();

            if (!request.IsValid)
                return Result.Invalid(request.ValidationResult.AsErrors());

            var user = await _repository.GetUserById(request.UserId);
            var newProfile = await _repositoryProfile.GetProfileById(request.ProfileId);

            if (user == null)
                return Result.NotFound("Usuário não encontrado");

            if (newProfile == null)
                return Result.NotFound("Perfil não encontrado");

            user.ProfileId = request.ProfileId;
            user.Profile = newProfile;

            _repository.Update(user);

            try
            {
                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                return Result.Error($"Ocorreu um erro durante a confirmação das alterações: {ex.Message}");
            }

            return Result.Success("Perfil alterado com sucesso");
        }
        catch (Exception ex)
        {
            return Result.Error($"Ocorreu um erro durante a validação: {ex.Message}");
        }
    }

    public async Task<Result<IPagedList<GetUserResponse[]>>> GetUsersAsync()
    {
        try
        {
            var users = await _repository.GetAll();

            var userResponses = users
                .Select(user => new GetUserResponse(user.Id, user.Email,
                    user.Username,
                    user.Avatar,
                    new ProfileDto(user.Profile.Id, user.Profile.ProfileName, user.Profile.Description),
                    user.CreatedAt
                ));

            var sortOrder = "desc";

            // Ordenação ascendente ou descendente com base no nome de usuário
            switch (sortOrder?.ToLower())
            {
                case "desc":
                    userResponses = userResponses.OrderByDescending(userResponse => userResponse.Username);
                    break;
                default:
                    userResponses = userResponses.OrderBy(userResponse => userResponse.Username);
                    break;
            }

            var pageNumber = 1;
            var pageSize = 10;

            var pagedUserResponses = userResponses.ToPagedList(pageNumber, pageSize);

            // Transforma itens individuais em arrays
            var pagedArrays = pagedUserResponses
                .Select(u => new GetUserResponse[] { u })
                .ToPagedList(pageNumber, pageSize);

            return Result.Success(pagedArrays);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
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