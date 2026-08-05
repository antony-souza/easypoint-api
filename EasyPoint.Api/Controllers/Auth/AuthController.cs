using EasyPoint.Application.UseCases.Auth.Login;
using EasyPoint.Application.UseCases.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Auth;

[ApiController]
[Route("auth")]
[AllowAnonymous]
public sealed class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCommand registerCommand,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(registerCommand, cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status201Created, result.Value)
            : BadRequest(result.Error);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand loginCommand,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(loginCommand, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : Unauthorized(result.Error);
    }
}
