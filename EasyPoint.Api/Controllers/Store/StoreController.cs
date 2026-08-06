using EasyPoint.Application.UseCases.Stores.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Store;

[ApiController]
[Route("/store")]
[Authorize]
public class StoreController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateStoreCommand createStoreCommand,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(createStoreCommand, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}
