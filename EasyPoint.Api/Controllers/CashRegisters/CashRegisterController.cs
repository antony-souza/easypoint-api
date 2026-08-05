using EasyPoint.Application.UseCases.CashRegisters.Create;
using EasyPoint.Application.UseCases.CashRegisters.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.CashRegisters;

[ApiController]
[Route("cash-registers")]
[Authorize]
public sealed class CashRegisterController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetCashRegistersQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCashRegisterCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? StatusCode(StatusCodes.Status201Created, result.Value)
            : BadRequest(result.Error);
    }
}
