namespace EasyPoint.Application.UseCases.StoreUsers.GetAll;

public sealed record GetAllStoreUsersResponse(
    Guid Id,
    Guid UserId,
    string UserName
);