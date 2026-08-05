using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Products.Create;

public sealed class CreateProductHandler(
    IProductRepository productRepository,
    ICurrentUser currentUser)
    : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    public async Task<Result<CreateProductResponse>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            BarCode = request.BarCode,
            CategoryId = request.CategoryId,
            StoreId = currentUser.StoreId
        };

        var createdProduct = await productRepository.CreateAsync(
            product,
            cancellationToken);

        var response = new CreateProductResponse(
            createdProduct.Id,
            createdProduct.Name,
            createdProduct.BarCode,
            createdProduct.CategoryId,
            createdProduct.StoreId);

        return Result<CreateProductResponse>.Success(response);
    }
}