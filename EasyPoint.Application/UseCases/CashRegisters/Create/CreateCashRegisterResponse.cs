namespace EasyPoint.Application.UseCases.CashRegisters.Create;

public sealed record CreateCashRegisterResponse(
    Guid Id,
    string Name,
    string Code,
    bool IsActive,
    Guid StoreId
);
