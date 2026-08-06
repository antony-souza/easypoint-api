using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Categories.Create;

public sealed record CreateCategoryCommand(
    string Name
) : IRequest<Result<CreateCategoryResponse>>;
