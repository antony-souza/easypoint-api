using EasyPoint.Application.UseCases.Organizations.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Organizations;

[ApiController]
[Route("organizations")]
public class OrganizationController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrganization(
        CreateOrganizationCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}