namespace EasyPoint.Application.UseCases.StoreUsers.Create;

public sealed record CreateStoreUserResponse(
    Guid Id,
    Guid OrganizationId,
    Guid StoreId,
    Guid UserId
);
