using System.Net.Mime;
using ERP.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using ERP.Api.Models;
using ERP.Application.Interfaces;
using ERP.Application.Requests.UsersRequests;
using ERP.Application.Services;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUsersService _service;

    public UserController(IUsersService service, INotificationService notificationService)
    {
        _service = service;
    }


    /// <summary>
    /// Efetua a autenticação.
    /// </summary>
    /// <param name="request">Endereço de e-mail e senha.</param>
    /// <response code="200">Retorna o token de acesso.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhuma conta é encontrado pelo e-mail e senha fornecido.</response>
    [HttpPost("add")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<AddUsersInWorkspaceRequest>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddUsersInMyWorkspace([FromBody]AddUsersInWorkspaceRequest request)
    {
        return (await _service.AddUsersAsync(request)).ToActionResult();
    }

}
