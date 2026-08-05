namespace EasyPoint.Application.UseCases.Products.Create;

public sealed record CreateProductResponse(
    Guid Id,
    string Name,
    string BarCode,
    Guid CategoryId,
    Guid StoreId
);