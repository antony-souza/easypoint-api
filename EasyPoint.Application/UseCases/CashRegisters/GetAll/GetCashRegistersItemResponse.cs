namespace EasyPoint.Application.UseCases.CashRegisters.GetAll;

public sealed record GetCashRegistersItemResponse(
    Guid Id,
    string Name,
    string Code,
    bool IsActive
);
