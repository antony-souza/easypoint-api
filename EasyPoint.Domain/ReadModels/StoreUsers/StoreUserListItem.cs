namespace EasyPoint.Domain.ReadModels.StoreUsers;

public sealed record StoreUserListItem(
    Guid Id,
    Guid UserId,
    string UserFullName
);