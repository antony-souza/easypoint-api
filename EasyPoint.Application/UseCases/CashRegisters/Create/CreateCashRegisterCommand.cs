using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.CashRegisters.Create;

public sealed record CreateCashRegisterCommand(
    Guid StoreId,
    string Name,
    string Code
) : IRequest<Result<CreateCashRegisterResponse>>;
