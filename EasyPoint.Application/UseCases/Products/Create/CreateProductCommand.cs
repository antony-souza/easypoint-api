using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Products.Create;

public sealed record Command(
    string Name,
    string BarCode,
    Guid CategoryId,
    Guid StoreId
) : IRequest<Result<Response>>;
