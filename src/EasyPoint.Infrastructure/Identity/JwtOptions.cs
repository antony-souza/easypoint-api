namespace EasyPoint.Infrastructure.Identity;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = "EasyPoint.Api";
    public string Audience { get; set; } = "EasyPoint.Client";
    public string Key { get; set; } = string.Empty;
    public int ExpirationInMinutes { get; set; } = 60;
}
