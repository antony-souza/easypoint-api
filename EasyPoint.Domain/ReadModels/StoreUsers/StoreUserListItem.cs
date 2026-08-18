namespace EasyPoint.Domain.ReadModels.StoreUsers;

public sealed record GetPagedByStoreItem(
    Guid Id,
    Guid UserId,
    string UserName
);