using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Pagination;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.StoreUsers.GetAll;

public sealed class GetAllStoreUsersHandler(
    IStoreUserRepository storeUserRepository,
    ICurrentUser currentUser
)
    : IRequestHandler<GetAllStoreUsersQuery, Result<PagedResponse<GetAllStoreUsersResponse>>>
{
    public async Task<Result<PagedResponse<GetAllStoreUsersResponse>>> Handle(
        GetAllStoreUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        if (!await currentUser.HasStoreAccessAsync(
                request.StoreId,
                cancellationToken))
        {
            return Result<PagedResponse<GetAllStoreUsersResponse>>.Failure(
                "O usuário não está vinculado a esta loja.");
        }

        var skip = (request.Page - 1) * request.PerPage;

        var (storeUsers, totalItems) = await storeUserRepository
            .GetPagedByStoreAsync(
                request.StoreId,
                skip,
                request.PerPage,
                cancellationToken);

        var items = storeUsers
            .Select(su => new GetAllStoreUsersResponse(
                su.Id,
                su.UserId,
                su.UserFullName
            )).ToList();

        return Result<PagedResponse<GetAllStoreUsersResponse>>.Success(
            new PagedResponse<GetAllStoreUsersResponse>(
                items,
                request.Page,
                request.PerPage,
                totalItems));
    }
}