using EasyPoint.Application.UseCases.StoreUsers.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.StoreUsers;

[ApiController]
[Route("store/{storeId:guid}/users")]
[Authorize]
public sealed class StoreUserController(ISender sender) : ControllerBase
{
    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> Create(
        Guid storeId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var command = new CreateStoreUserCommand(storeId, userId);
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status201Created, result.Value)
            : BadRequest(result.Error);
    }
}
