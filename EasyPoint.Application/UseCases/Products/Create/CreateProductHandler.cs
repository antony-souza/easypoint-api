using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Products.Create;

public sealed class CreateProductHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    ICurrentUser currentUser)
    : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    public async Task<Result<CreateProductResponse>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var organizationId = currentUser.OrganizationId;
        var category = await categoryRepository.GetByIdAsync(
            request.CategoryId,
            cancellationToken);

        if (category is null || category.OrganizationId != organizationId)
        {
            return Result<CreateProductResponse>.Failure(
                "A categoria não pertence à organização do usuário.");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            BarCode = request.BarCode.Trim(),
            CategoryId = request.CategoryId,
            OrganizationId = organizationId
        };

        var createdProduct = await productRepository.CreateAsync(
            product,
            cancellationToken);

        var response = new CreateProductResponse(
            createdProduct.Id,
            createdProduct.Name,
            createdProduct.BarCode,
            createdProduct.CategoryId,
            createdProduct.OrganizationId);

        return Result<CreateProductResponse>.Success(response);
    }
}
