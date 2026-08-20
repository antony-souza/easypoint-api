using EasyPoint.Application.UseCases.Users.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Users;

[ApiController]
[Route("users")]
[Authorize]
public sealed class UsersController(ISender sender) : ControllerBase
{
    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> Update(
        Guid userId,
        [FromBody] UpdateUsersRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUsersCommand(
            userId,
            request.Name,
            request.Username,
            request.Email);
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    public sealed record UpdateUsersRequest(
        string Name,
        string Username,
        string Email);
}
