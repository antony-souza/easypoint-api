using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Users.Update;

public sealed record UpdateUsersCommand(
    Guid UserId,
    string Name,
    string Username,
    string Email
) : IRequest<Result<UpdateUsersResponse>>;
