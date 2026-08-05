using EasyPoint.Application.Common.Pagination;
using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Products.GetAll;

public sealed record GetProductsQuery(
    int Page = 1,
    int PerPage = 10
) : IRequest<Result<PagedResponse<GetProductsItemResponse>>>;
