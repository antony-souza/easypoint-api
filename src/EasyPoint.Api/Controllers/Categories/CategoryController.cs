using EasyPoint.Application.UseCases.Categories.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Categories;

[ApiController]
[Route("categories")]
[Authorize]
public class CategoryController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCategory(
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