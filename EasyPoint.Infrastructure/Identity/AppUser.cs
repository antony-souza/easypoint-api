using Microsoft.AspNetCore.Identity;

namespace EasyPoint.Infrastructure.Identity;

public sealed class AppUser : IdentityUser<Guid>
{
    public Guid StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
}