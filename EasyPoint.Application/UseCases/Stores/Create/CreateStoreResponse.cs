namespace EasyPoint.Application.UseCases.Stores.Create;

public sealed record CreateStoreResponse(
    Guid Id,
    string Name,
    string Cnpj,
    string Organization
);