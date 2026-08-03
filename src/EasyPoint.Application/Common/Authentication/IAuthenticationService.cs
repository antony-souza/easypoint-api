namespace EasyPoint.Application.Common.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> RegisterAsync(
        Guid storeId,
        string name,
        string userName,
        string email,
        string password,
        CancellationToken cancellationToken = default);

    Task<AuthenticationResponse?> LoginAsync(
        Guid storeId,
        string email,
        string password,
        CancellationToken cancellationToken = default);
}
