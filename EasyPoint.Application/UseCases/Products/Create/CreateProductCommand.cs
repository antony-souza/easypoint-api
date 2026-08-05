using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Products.Create;

public sealed record CreateProductCommand(
    string Name,
    string BarCode,
    Guid CategoryId
) : IRequest<Result<CreateProductResponse>>;
