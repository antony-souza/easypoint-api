using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.StoreUsers;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.StoreUsers.Create;

public sealed class CreateStoreUserHandler(
    IStoreRepository storeRepository,
    IStoreUserRepository storeUserRepository,
    ICurrentUser currentUser)
    : IRequestHandler<
        CreateStoreUserCommand,
        Result<CreateStoreUserResponse>>
{
    public async Task<Result<CreateStoreUserResponse>> Handle(
        CreateStoreUserCommand request,
        CancellationToken cancellationToken)
    {
        var store = await storeRepository.GetByIdAsync(
            request.StoreId,
            cancellationToken);

        if (store is null)
        {
            return Result<CreateStoreUserResponse>.Failure(
                "A loja não foi encontrada.");
        }

        if (store.OrganizationId != currentUser.OrganizationId)
        {
            return Result<CreateStoreUserResponse>.Failure(
                "A loja não pertence à organização do usuário.");
        }

        var userBelongsToOrganization = await storeUserRepository
            .UserBelongsToOrganizationAsync(
                request.UserId,
                currentUser.OrganizationId,
                cancellationToken);

        if (!userBelongsToOrganization)
        {
            return Result<CreateStoreUserResponse>.Failure(
                "O usuário não pertence à organização da loja.");
        }

        var existingLink = await storeUserRepository.GetByStoreAndUserAsync(
            request.StoreId,
            request.UserId,
            cancellationToken);

        if (existingLink is not null)
        {
            return Result<CreateStoreUserResponse>.Failure(
                "O usuário já está vinculado a esta loja.");
        }

        var storeUser = new StoreUser
        {
            Id = Guid.NewGuid(),
            OrganizationId = currentUser.OrganizationId,
            StoreId = request.StoreId,
            UserId = request.UserId
        };

        var createdLink = await storeUserRepository.CreateAsync(
            storeUser,
            cancellationToken);

        return Result<CreateStoreUserResponse>.Success(
            new CreateStoreUserResponse(
                Id: createdLink.Id,
                OrganizationId: createdLink.OrganizationId,
                StoreId: createdLink.StoreId,
                UserId: createdLink.UserId));
    }
}
