using EasyPoint.Application.UseCases.Products.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Products;

[ApiController]
[Route("products")]
public sealed class ProductController(ISender sender)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] Command command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            command,
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}