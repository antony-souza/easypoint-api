using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Categories.Create;

public sealed record Command(
    Guid StoreId,
    string Name
) : IRequest<Result<Response>>;