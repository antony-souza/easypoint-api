using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Auth.Login;

public sealed record Command(
    Guid StoreId,
    string Email,
    string Password
) : IRequest<Result<AuthenticationResponse>>;
