using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Auth.Login;

public sealed class Handler(IAuthenticationService authenticationService)
    : IRequestHandler<Command, Result<AuthenticationResponse>>
{
    public async Task<Result<AuthenticationResponse>> Handle(
        Command request,
        CancellationToken cancellationToken)
    {
        if (request.StoreId == Guid.Empty)
            return Result<AuthenticationResponse>.Failure("StoreId is required.");

        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return Result<AuthenticationResponse>.Failure("Email and password are required.");

        var response = await authenticationService.LoginAsync(
            request.StoreId,
            request.Email,
            request.Password,
            cancellationToken);

        return response is null
            ? Result<AuthenticationResponse>.Failure("Invalid email or password.")
            : Result<AuthenticationResponse>.Success(response);
    }
}
