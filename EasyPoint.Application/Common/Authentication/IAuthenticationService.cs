using EasyPoint.Domain.Entities.Users;

namespace EasyPoint.Application.Common.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> RegisterAsync(
        Guid organizationId,
        string name,
        string userName,
        string email,
        string password,
        CancellationToken cancellationToken = default);

    Task<AuthenticationResponse?> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);

    Task<AppUser?> UpdateUserAsync(
        Guid userId,
        Guid organizationId,
        string name,
        string userName,
        string email,
        CancellationToken cancellationToken = default);
}
