namespace EasyPoint.Application.Common.Authentication;

public interface ICurrentUser
{
    Guid UserId { get; }
    Guid OrganizationId { get; }
    Task<bool> HasStoreAccessAsync(
        Guid storeId,
        CancellationToken cancellationToken = default);
}
