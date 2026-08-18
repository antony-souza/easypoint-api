using EasyPoint.Application.Common.Pagination;
using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.StoreUsers.GetAll;

public sealed record GetAllStoreUsersQuery(
    Guid StoreId,
    int Page = 1,
    int PerPage = 10
) : IRequest<Result<PagedResponse<GetAllStoreUsersResponse>>>;
