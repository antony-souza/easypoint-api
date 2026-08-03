namespace EasyPoint.Application.UseCases.Products.Create;

public sealed record Response(
    Guid Id,
    string Name,
    string BarCode,
    Guid CategoryId,
    Guid StoreId
);