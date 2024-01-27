using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using ERP.Application.Interfaces;
using ERP.Application.Requests.UsersRequests;
using ERP.Application.Services;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;
using ERP.Shared.Abstractions;
using Moq;
using OpenTelemetry.Trace;
using Xunit;

namespace ERP.Tests.Application.Services;

public class UsersServiceTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<INotificationService> _notificationServiceMock;
    private readonly Mock<IProfileRepository> _profileRepositoryMock;

    public UsersServiceTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _notificationServiceMock = new Mock<INotificationService>();
        _profileRepositoryMock = new Mock<IProfileRepository>();

        // Usamos Mock.Of para instanciar uma instância de alguma interface
        _userService = new UserService(
            dateTimeService:        Mock.Of<IDateTimeService>(),
            tokenClaimsService:     Mock.Of<ITokenClaimsService>(),
            uow:                    Mock.Of<IUnitOfWork>(),
            hashService:            Mock.Of<IHashService>(),
            notificationService:    Mock.Of<INotificationService>(),
            repository:             _userRepositoryMock.Object,
            repositoryProfile:      _profileRepositoryMock.Object
        );
    }

    [Fact]
    public async Task ChangeProfile_ReturnsSuccess()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var profileId = 1; // Defina o ID do perfil

        var user = new User("jonhDoe@pivot.com", "Jonh Doe", "12345", 1);
        _userRepositoryMock.Setup(repo => repo.GetUserById(userId))
            .ReturnsAsync(user); // Configura o mock para retornar o usuário criado

        _profileRepositoryMock.Setup(repo => repo.GetProfileById(profileId))
            .ReturnsAsync(new Profile()); // Configura o mock para retornar um perfil simulado

        var request = new ChangeProfileRequest(userId, profileId);

        // Act
        var result = await _userService.ChangeProfile(request);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ChangeProfile_ReturnsError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var profileId = 1; // Defina o ID do perfil

        var user = new User("jonhDoe@pivot.com", "Jonh Doe", "12345", 1);
        _userRepositoryMock.Setup(repo => repo.GetUserById(userId))
            .ReturnsAsync(user); // Configura o mock para retornar o usuário criado

        _profileRepositoryMock.Setup(repo => repo.GetProfileById(profileId))
            .ReturnsAsync(new Profile()); // Configura o mock para retornar um perfil simulado

        var request = new ChangeProfileRequest(userId, 2);

        // Act
        var result = await _userService.ChangeProfile(request);

        // Assert
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveUserInWorkspace_ReturnsSuccess()
    {
        var userId = new Guid();
        var userAuth = new ClaimsPrincipal();
        var user = new User("jonhDoe@pivot.com", "Jonh Doe", "12345", 1);

        _userRepositoryMock.Setup(repo => repo.GetUserById(userId))
            .ReturnsAsync(user);

        var result = await _userService.RemoveUserInWorkspace(userId, userAuth);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task RemoveUserInWorkspace_ReturnsNotFound()
    {
        var userId = new Guid();
        var userAuth = new ClaimsPrincipal();

        _userRepositoryMock.Setup(repo => repo.GetUserById(userId))
            .ReturnsAsync((User)null);

        var result = await _userService.RemoveUserInWorkspace(userId, userAuth);

        Assert.False(result.IsSuccess);
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }

    [Fact]
    public async Task RemoveUserInWorkspace_ReturnsError()
    {
        var userId = new Guid();
        var userAuth = new ClaimsPrincipal();

        _userRepositoryMock.Setup(repo => repo.GetUserById(userId))
            .Throws<Exception>();

        var result = await _userService.RemoveUserInWorkspace(userId, userAuth);

        Assert.False(result.IsSuccess);
        Assert.Contains("Ocorreu um erro durante a validação:", result.Errors.FirstOrDefault());
    }
}