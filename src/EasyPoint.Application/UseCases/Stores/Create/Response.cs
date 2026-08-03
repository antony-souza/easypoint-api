namespace EasyPoint.Application.UseCases.Stores.Create;

public sealed record Response(
    Guid Id,
    string Name
);