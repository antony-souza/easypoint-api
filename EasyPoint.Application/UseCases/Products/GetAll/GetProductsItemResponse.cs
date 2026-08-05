namespace EasyPoint.Application.UseCases.Products.GetAll;

public sealed record GetProductsItemResponse(
    Guid Id,
    string Name,
    string BarCode,
    string Category,
    string Store);
