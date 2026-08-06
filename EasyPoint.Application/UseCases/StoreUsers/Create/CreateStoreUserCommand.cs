using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.StoreUsers.Create;

public sealed record CreateStoreUserCommand(
    Guid StoreId,
    Guid UserId
) : IRequest<Result<CreateStoreUserResponse>>;
