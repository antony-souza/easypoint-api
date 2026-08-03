using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Stores.Create;

public sealed record Command(
    string Name
) : IRequest<Result<Response>>;