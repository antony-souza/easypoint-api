using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EasyPoint.Application.Common.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EasyPoint.Infrastructure.Identity;

public sealed class AuthenticationService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IOptions<JwtOptions> jwtOptions) : IAuthenticationService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<AuthenticationResponse> RegisterAsync(
        Guid storeId,
        string name,
        string userName,
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var normalizedUserName = userManager.NormalizeName(userName.Trim());
        var normalizedEmail = userManager.NormalizeEmail(email.Trim());

        var userNameAlreadyExists = await userManager.Users.AnyAsync(
            user => user.StoreId == storeId && user.NormalizedUserName == normalizedUserName,
            cancellationToken);

        if (userNameAlreadyExists)
            throw new InvalidOperationException("Já existe um usuário com este nome de usuário nesta loja.");

        var emailAlreadyExists = await userManager.Users.AnyAsync(
            user => user.StoreId == storeId && user.NormalizedEmail == normalizedEmail,
            cancellationToken);

        if (emailAlreadyExists)
            throw new InvalidOperationException("Já existe um usuário cadastrado com este e-mail nesta loja.");

        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            StoreId = storeId,
            Name = name.Trim(),
            UserName = userName.Trim(),
            Email = email.Trim()
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new InvalidOperationException(string.Join(" ", result.Errors.Select(error => error.Description)));

        return await CreateResponseAsync(user);
    }

    public async Task<AuthenticationResponse?> LoginAsync(
        Guid storeId,
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = userManager.NormalizeEmail(email.Trim());
        var user = await userManager.Users.SingleOrDefaultAsync(
            item => item.StoreId == storeId && item.NormalizedEmail == normalizedEmail,
            cancellationToken);

        if (user is null)
            return null;

        var result = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
        return result.Succeeded ? await CreateResponseAsync(user) : null;
    }

    private Task<AuthenticationResponse> CreateResponseAsync(AppUser user)
    {
        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new("store_id", user.StoreId.ToString())
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expiresAt.UtcDateTime,
            signingCredentials: credentials);

        return Task.FromResult(new AuthenticationResponse(
            new JwtSecurityTokenHandler().WriteToken(token)));
    }
}
