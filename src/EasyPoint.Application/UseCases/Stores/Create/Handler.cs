using System.Text.RegularExpressions;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Entities.Stores;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Stores.Create;

public class Handler(IStoreRepository storeRepository) : IRequestHandler<Command, Result<Response>>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result<Response>.Failure("Name is required");
        }

        var cnpj = request.Cnpj.Trim();
        var isValidCnpjFormat = Regex.IsMatch(cnpj, @"^\d{14}$") ||
                                Regex.IsMatch(cnpj, @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$");

        if (!isValidCnpjFormat)
        {
            return Result<Response>.Failure(
                "CNPJ must contain 14 digits, with or without the mask 00.000.000/0000-00.");
        }

        var existingStore = await storeRepository.GetByCnpjAsync(cnpj, cancellationToken);

        if (existingStore is not null)
        {
            return Result<Response>.Failure(
                "Já existe uma loja cadastrada com este CNPJ.");
        }

        var store = new Store
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Cnpj = cnpj
        };

        var createdStore = await storeRepository.CreateAsync(store, cancellationToken);

        var response = new Response(
            Id: createdStore.Id,
            Name: createdStore.Name,
            Cnpj: createdStore.Cnpj);

        return Result<Response>.Success(response);
    }
}
