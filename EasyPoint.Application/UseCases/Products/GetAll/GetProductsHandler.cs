using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Pagination;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Products.GetAll;

public sealed class GetProductsHandler(
    IProductRepository productRepository,
    ICurrentUser currentUser)
    : IRequestHandler<GetProductsQuery, Result<PagedResponse<GetProductsItemResponse>>>
{
    public async Task<Result<PagedResponse<GetProductsItemResponse>>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var skip = (request.Page - 1) * request.PerPage;

        var (products, totalItems) = await productRepository.GetPagedByStoreAsync(
            currentUser.StoreId,
            skip,
            request.PerPage,
            cancellationToken);

        var items = products
            .Select(product => new GetProductsItemResponse(
                    Id: product.Id,
                    Name: product.Name,
                    BarCode: product.BarCode,
                    Category: product.Category.Name,
                    Store: product.Store.Name
                )
            )
            .ToList();

        var response = new PagedResponse<GetProductsItemResponse>(
            items,
            request.Page,
            request.PerPage,
            totalItems);

        return Result<PagedResponse<GetProductsItemResponse>>.Success(response);
    }
}
