using EasyPoint.Application.UseCases.Products.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Products;

[ApiController]
[Route("products")]
[Authorize]
public sealed class ProductController(ISender mediator)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] Command command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            command,
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}
