using EasyPoint.Application.Common.Results;
using EasyPoint.Application.Common.Authentication;
using MediatR;

namespace EasyPoint.Application.UseCases.Users.Update;

public sealed class UpdateUsersHandler(
    ICurrentUser currentUser,
    IAuthenticationService authenticationService)
    : IRequestHandler<UpdateUsersCommand, Result<UpdateUsersResponse>>
{
    public async Task<Result<UpdateUsersResponse>> Handle(
        UpdateUsersCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var user = await authenticationService.UpdateUserAsync(
                request.UserId,
                currentUser.OrganizationId,
                request.Name,
                request.Username,
                request.Email,
                cancellationToken);

            if (user is null)
                return Result<UpdateUsersResponse>.Failure(
                    "Usuário não encontrado na sua organização.");

            return Result<UpdateUsersResponse>.Success(new UpdateUsersResponse(
                user.Id,
                user.Name,
                user.UserName!,
                user.Email!));
        }
        catch (InvalidOperationException exception)
        {
            return Result<UpdateUsersResponse>.Failure(exception.Message);
        }
    }
}
