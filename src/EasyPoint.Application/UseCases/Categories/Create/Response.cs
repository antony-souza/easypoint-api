namespace EasyPoint.Application.UseCases.Categories.Create;

public sealed record Response(
    Guid Id,
    string Name,
    Guid StoreId
);