using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Stores.Create;

public sealed class CreateStoreHandler(
    IStoreRepository storeRepository,
    IOrganizationRepository organizationRepository,
    ICurrentUser currentUser)
    : IRequestHandler<CreateStoreCommand, Result<CreateStoreResponse>>
{
    public async Task<Result<CreateStoreResponse>> Handle(
        CreateStoreCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result<CreateStoreResponse>.Failure("Name is required");
        }

        var organization = await organizationRepository.GetByIdAsync(
            currentUser.OrganizationId,
            cancellationToken);

        if (organization is null)
        {
            return Result<CreateStoreResponse>.Failure(
                "A organização do usuário não foi encontrada.");
        }

        var existingStore = await storeRepository.GetByCnpjAsync(
            request.Cnpj,
            cancellationToken);

        if (existingStore is not null)
        {
            return Result<CreateStoreResponse>.Failure(
                "Já existe uma loja cadastrada com este CNPJ.");
        }

        var store = new Store
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Cnpj = request.Cnpj,
            OrganizationId = organization.Id,
        };

        var createdStore = await storeRepository.CreateAsync(
            store,
            cancellationToken);

        var response = new CreateStoreResponse(
            Id: createdStore.Id,
            Name: createdStore.Name,
            Cnpj: createdStore.Cnpj,
            Organization: organization.Name
        );

        return Result<CreateStoreResponse>.Success(response);
    }
}