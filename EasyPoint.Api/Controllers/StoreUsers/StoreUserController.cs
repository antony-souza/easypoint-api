using EasyPoint.Application.UseCases.StoreUsers.Create;
using EasyPoint.Application.UseCases.StoreUsers.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.StoreUsers;

[ApiController]
[Route("store/{storeId:guid}/users")]
[Authorize]
public sealed class StoreUserController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        Guid storeId,
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllStoreUsersQuery(storeId, page, perPage);
        var result = await sender.Send(query, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

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
