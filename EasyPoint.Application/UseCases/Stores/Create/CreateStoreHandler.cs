using System.Text.RegularExpressions;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Stores.Create;

public class CreateStoreHandler(IStoreRepository storeRepository)
    : IRequestHandler<CreateStoreCommand, Result<CreateStoreResponse>>
{
    public async Task<Result<CreateStoreResponse>> Handle(CreateStoreCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result<CreateStoreResponse>.Failure("Name is required");
        }

        var existingStore = await storeRepository.GetByCnpjAsync(request.Cnpj, cancellationToken);

        if (existingStore is not null)
        {
            return Result<CreateStoreResponse>.Failure(
                "Já existe uma loja cadastrada com este CNPJ.");
        }

        var store = new Store
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Cnpj = request.Cnpj
        };

        var createdStore = await storeRepository.CreateAsync(store, cancellationToken);

        var response = new CreateStoreResponse(
            Id: createdStore.Id,
            Name: createdStore.Name,
            Cnpj: createdStore.Cnpj);

        return Result<CreateStoreResponse>.Success(response);
    }
}