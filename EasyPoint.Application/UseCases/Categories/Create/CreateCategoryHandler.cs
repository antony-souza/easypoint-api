using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Categories.Create;

public class Handler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, Result<Response>>
{
    public async Task<Result<Response>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result<Response>.Failure(
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

        var response = new Response(
            Id: createCategory.Id,
            Name: createCategory.Name,
            StoreId: createCategory.StoreId
        );

        return Result<Response>.Success(response);
    }
}