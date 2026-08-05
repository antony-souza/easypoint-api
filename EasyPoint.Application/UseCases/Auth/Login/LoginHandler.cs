using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using MediatR;

namespace EasyPoint.Application.UseCases.Auth.Login;

public sealed class LoginHandler(IAuthenticationService authenticationService)
    : IRequestHandler<LoginCommand, Result<AuthenticationResponse>>
{
    public async Task<Result<AuthenticationResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await authenticationService.LoginAsync(
                request.Email,
                request.Password,
                cancellationToken);

            return response is null
                ? Result<AuthenticationResponse>.Failure("Invalid email or password.")
                : Result<AuthenticationResponse>.Success(response);
        }
        catch (InvalidOperationException exception)
        {
            return Result<AuthenticationResponse>.Failure(exception.Message);
        }
    }
}
