using EasyPoint.Application.UseCases.Stores.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Store;

[ApiController]
[Route("/store")]
public class StoreController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        [FromBody] Command command,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}