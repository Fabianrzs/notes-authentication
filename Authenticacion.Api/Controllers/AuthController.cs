using Authentication.Aplication.UseCase.Auth.Commands.Authentication;
using Authentication.Aplication.UseCase.Auth.Commands.Credential;
using Authentication.Aplication.UseCase.Auth.Commands.Register;
using Authentication.Aplication.UseCase.Auth.Dto;
using Reservar.Common.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MediatR;

namespace Authenticacion.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Auth Users.
    /// </summary>
    /// <response code="401">Invalid Credencials.</response>
    /// <response code="400">Invalid Request.</response>
    /// <response code="200">Sussess Authentitication.</response>
    [HttpPost("Authentication")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<AuthenticationDto>), StatusCodes.Status200OK)]
    public async Task<Response<AuthenticationDto>> Authentication(AuthenticationCommand command)
    {
        var response = await _mediator.Send(command);
        return new Response<AuthenticationDto>(response);
    }

    /// <summary>
    /// Auth Google Credencials.
    /// </summary>
    /// <response code="400">Invalid Request.</response>
    /// <response code="200">Sussess Authentitication.</response>
    [HttpPost("CredentialsGoogle")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<AuthenticationDto>), StatusCodes.Status200OK)]
    public async Task<Response<AuthenticationDto>> CredentialsGoogle(CredencialCommand command)
    {
        var response = await _mediator.Send(command);
        return new Response<AuthenticationDto>(response);
    }


    /// <summary>
    /// Auth Google Credencials.
    /// </summary>
    /// <response code="400">Invalid Request.</response>
    /// <response code="403">Invalid Refres.</response>
    /// <response code="200">Sussess Authentitication.</response>
    [HttpPost("RefreshToken")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<Response<string>> RefreshToken()
    {
        var id = User.FindFirstValue("Uid");
        var command = new RefreshCommand(new Guid(id));
        var response = await _mediator.Send(command);
        return new Response<String>(new String(response), "");
    }

    /// <summary>
    /// register Users.
    /// </summary>
    /// <response code="400">Invalid Request.</response>
    /// <response code="200">Sussess register.</response>
    [HttpPost("Register")]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    public async Task<Response<string>> Register(RegisterCommand command)
    {
        var response = await _mediator.Send(command);
        return new Response<String>(new String(response), "");
    }


}
