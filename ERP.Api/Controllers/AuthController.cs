using System.Net.Mime;
using ERP.Application.Services;
using Microsoft.AspNetCore.Mvc;
using ERP.Api.Extensions;
using ERP.Application.Interfaces;
using ERP.Application.Requests.AuthenticationRequests;
using ERP.Shared.Messages;

namespace ERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _service;

    public AuthController(IAuthenticationService service) =>
        _service = service;

    /// <summary>
    /// Efetua a autenticação.
    /// </summary>
    /// <param name="request">Endereço de e-mail e senha.</param>
    /// <response code="200">Retorna o token de acesso.</response>
    /// <response code="400">Retorna lista de erros, se a requisição for inválida.</response>
    /// <response code="404">Quando nenhuma conta é encontrado pelo e-mail e senha fornecido.</response>
    [HttpPost("authenticate")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Authenticate([FromBody]LogInRequest request)
    {
        return (await _service.AuthenticateAsync(request)).ToActionResult();
    }
}
