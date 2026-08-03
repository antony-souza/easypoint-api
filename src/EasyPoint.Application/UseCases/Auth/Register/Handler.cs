using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Auth.Register;

public sealed class Handler(
    IStoreRepository storeRepository,
    IAuthenticationService authenticationService)
    : IRequestHandler<Command, Result<AuthenticationResponse>>
{
    public async Task<Result<AuthenticationResponse>> Handle(
        Command request,
        CancellationToken cancellationToken)
    {
        if (request.StoreId == Guid.Empty)
            return Result<AuthenticationResponse>.Failure("StoreId is required.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return Result<AuthenticationResponse>.Failure("Name is required.");

        if (string.IsNullOrWhiteSpace(request.UserName))
            return Result<AuthenticationResponse>.Failure("UserName is required.");

        if (string.IsNullOrWhiteSpace(request.Email))
            return Result<AuthenticationResponse>.Failure("Email is required.");

        if (string.IsNullOrWhiteSpace(request.Password))
            return Result<AuthenticationResponse>.Failure("Password is required.");

        var store = await storeRepository.GetByIdAsync(request.StoreId, cancellationToken);

        if (store is null)
            return Result<AuthenticationResponse>.Failure("Store was not found.");

        try
        {
            var response = await authenticationService.RegisterAsync(
                request.StoreId,
                request.Name,
                request.UserName,
                request.Email,
                request.Password,
                cancellationToken);

            return Result<AuthenticationResponse>.Success(response);
        }
        catch (InvalidOperationException exception)
        {
            return Result<AuthenticationResponse>.Failure(exception.Message);
        }
    }
}
