using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.Categories;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Categories.Create;

public sealed class CreateCategoryHandler(
    ICategoryRepository categoryRepository,
    ICurrentUser currentUser)
    : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryResponse>>
{
    public async Task<Result<CreateCategoryResponse>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            OrganizationId = currentUser.OrganizationId
        };

        var createdCategory = await categoryRepository.CreateAsync(
            category,
            cancellationToken);

        var response = new CreateCategoryResponse(
            Id: createdCategory.Id,
            Name: createdCategory.Name,
            OrganizationId: createdCategory.OrganizationId);

        return Result<CreateCategoryResponse>.Success(response);
    }
}
