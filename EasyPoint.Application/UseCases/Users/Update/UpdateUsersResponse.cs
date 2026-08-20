namespace EasyPoint.Application.UseCases.Users.Update;

public sealed record UpdateUsersResponse(
    Guid Id,
    string Name,
    string Username,
    string Email);
