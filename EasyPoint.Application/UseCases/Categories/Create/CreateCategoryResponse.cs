namespace EasyPoint.Application.UseCases.Categories.Create;

public sealed record CreateCategoryResponse(
    Guid Id,
    string Name,
    Guid StoreId
);