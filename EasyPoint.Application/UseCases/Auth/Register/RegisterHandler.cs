using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Auth.Register;

public sealed class Handler(
    IStoreRepository storeRepository,
    IAuthenticationService authenticationService)
    : IRequestHandler<RegisterCommand, Result<AuthenticationResponse>>
{
    public async Task<Result<AuthenticationResponse>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
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
