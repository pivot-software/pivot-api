using System.Net.Mime;
using ERP.Api.Extensions;
using ERP.Api.Models;
using ERP.Application.Interfaces;
using ERP.Application.Requests.WorkspaceRequests;
using ERP.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class WorkspaceController : ControllerBase
{
    private readonly IWorkspaceServices _service;

    public WorkspaceController(IWorkspaceServices service) =>
        _service = service;

    /// <summary>
    /// Criar um Workspace
    /// </summary>
    /// <param name="request">busiss_name, business_logo.</param>
    /// <response code="200">Retorna o token de acesso.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhuma conta é encontrado pelo e-mail e senha fornecido.</response>
    [HttpPost("create")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<ConfigWorkspaceResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateWorkspace([FromBody]CreateWorkspaceRequest request)
    {
        var user = HttpContext.User;

        return (await _service.CreateWorkspaceAsync(request, user)).ToActionResult();
    }
}
