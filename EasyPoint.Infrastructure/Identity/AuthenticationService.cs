using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EasyPoint.Application.Common.Authentication;
using EasyPoint.Domain.Entities.Users;
using EasyPoint.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EasyPoint.Infrastructure.Identity;

public sealed class AuthenticationService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    EasyPointDbContext context,
    IOptions<JwtOptions> jwtOptions) : IAuthenticationService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<AuthenticationResponse> RegisterAsync(
        Guid organizationId,
        string name,
        string userName,
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var organizationExists = await context.Organizations
            .AnyAsync(
                organization => organization.Id == organizationId,
                cancellationToken);

        if (!organizationExists)
            throw new InvalidOperationException("Organization was not found.");

        var normalizedUserName = userManager.NormalizeName(userName.Trim());
        var normalizedEmail = userManager.NormalizeEmail(email.Trim());

        var userNameAlreadyExists = await userManager.Users.AnyAsync(
            user => user.NormalizedUserName == normalizedUserName,
            cancellationToken);

        if (userNameAlreadyExists)
            throw new InvalidOperationException(
                "Já existe um usuário com este nome de usuário.");

        var emailAlreadyExists = await userManager.Users.AnyAsync(
            user => user.NormalizedEmail == normalizedEmail,
            cancellationToken);

        if (emailAlreadyExists)
            throw new InvalidOperationException(
                "Já existe um usuário cadastrado com este e-mail.");

        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            OrganizationId = organizationId,
            Name = name.Trim(),
            UserName = userName.Trim(),
            Email = email.Trim()
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(" ", result.Errors.Select(error => error.Description)));
        }

        return CreateResponse(user);
    }

    public async Task<AuthenticationResponse?> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = userManager.NormalizeEmail(email.Trim());

        var user = await userManager.Users
            .SingleOrDefaultAsync(
                item => item.NormalizedEmail == normalizedEmail,
                cancellationToken);

        if (user is null)
            return null;

        var organizationExists = await context.Organizations
            .AnyAsync(
                organization => organization.Id == user.OrganizationId,
                cancellationToken);

        if (!organizationExists)
            return null;

        var result = await signInManager.CheckPasswordSignInAsync(
            user,
            password,
            lockoutOnFailure: true);

        return result.Succeeded ? CreateResponse(user) : null;
    }

    private AuthenticationResponse CreateResponse(AppUser user)
    {
        var expiresAt = DateTimeOffset.UtcNow
            .AddMinutes(_jwtOptions.ExpirationInMinutes);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new("organization_id", user.OrganizationId.ToString())
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

        return new AuthenticationResponse(
            new JwtSecurityTokenHandler().WriteToken(token));
    }
}
