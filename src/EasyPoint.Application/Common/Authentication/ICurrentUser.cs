namespace EasyPoint.Application.Common.Authentication;

public interface ICurrentUser
{
    Guid UserId { get; }
    Guid StoreId { get; }
}
