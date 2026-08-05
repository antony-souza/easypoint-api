using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Stores.Create;

public sealed record CreateStoreCommand(
    string Name,
    string Cnpj
) : IRequest<Result<CreateStoreResponse>>;