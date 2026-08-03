using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.Products;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Products.Create;

public sealed class Handler(
    IProductRepository productRepository)
    : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(
        Command request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result<Response>.Failure(
                "O nome do produto é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(request.BarCode))
        {
            return Result<Response>.Failure(
                "O código de barras é obrigatório.");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            BarCode = request.BarCode,
            CategoryId = request.CategoryId,
            StoreId = request.StoreId
        };

        var createdProduct = await productRepository.CreateAsync(
            product,
            cancellationToken);

        var response = new Response(
            createdProduct.Id,
            createdProduct.Name,
            createdProduct.BarCode,
            createdProduct.CategoryId,
            createdProduct.StoreId);

        return Result<Response>.Success(response);
    }
}
