using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Categories.Create;

public class CreateCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryResponse>>
{
    public async Task<Result<CreateCategoryResponse>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result<CreateCategoryResponse>.Failure(
                "O nome do produto é obrigatório.");
        }

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            StoreId = request.StoreId
        };

        var createCategory = await categoryRepository.CreateAsync(
            category,
            cancellationToken);

        var response = new CreateCategoryResponse(
            Id: createCategory.Id,
            Name: createCategory.Name,
            StoreId: createCategory.StoreId
        );

        return Result<CreateCategoryResponse>.Success(response);
    }
}