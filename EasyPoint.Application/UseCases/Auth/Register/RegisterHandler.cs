using EasyPoint.Application.Common.Authentication;
using EasyPoint.Application.Common.Results;
using EasyPoint.Domain.Repositories;
using MediatR;

namespace EasyPoint.Application.UseCases.Auth.Register;

public sealed class RegisterHandler(
    IOrganizationRepository organizationRepository,
    IAuthenticationService authenticationService)
    : IRequestHandler<RegisterCommand, Result<AuthenticationResponse>>
{
    public async Task<Result<AuthenticationResponse>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var organization = await organizationRepository.GetByIdAsync(
            request.OrganizationId,
            cancellationToken);

        if (organization is null)
            return Result<AuthenticationResponse>.Failure(
                "Organization was not found.");

        try
        {
            var response = await authenticationService.RegisterAsync(
                request.OrganizationId,
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
