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
            return Result<Response>.Failure(
                "Name is required");
        }

        var store = new Store
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        var createdStore = await storeRepository.CreateAsync(
            store,
            cancellationToken
        );

        var response = new Response(
            createdStore.Id,
            createdStore.Name
        );

        return Result<Response>.Success(response);
    }
}