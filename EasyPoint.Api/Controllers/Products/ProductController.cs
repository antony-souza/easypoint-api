using EasyPoint.Application.UseCases.Products.Create;
using EasyPoint.Application.UseCases.Products.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Products;

[ApiController]
[Route("products")]
[Authorize]
public sealed class ProductController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetProductsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductCommand createProductCommand,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            createProductCommand,
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}
