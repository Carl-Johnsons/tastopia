using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using DuendeIdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DuendeIdentityServer.Services;

public class ProfileService : IProfileService
{
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimFactory;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileService(IUserClaimsPrincipalFactory<ApplicationUser> claimFactory, UserManager<ApplicationUser> userManager)
    {
        _claimFactory = claimFactory;
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        if (user == null)
        {
            return;
        }
        var principal = await _claimFactory.CreateAsync(user);

        var claims = principal.Claims.ToList();
        claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

        var roles = await _userManager.GetRolesAsync(user);
        // Remove default user claim
        RemoveClaim(claims, JwtClaimTypes.Name);

        // Add user claim
        // Standard claim
        claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName ?? ""));
        claims.Add(new Claim(JwtClaimTypes.Name, user.Name ?? ""));
        claims.Add(new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber ?? ""));
        claims.Add(new Claim(JwtClaimTypes.Email, user.Email ?? ""));
        claims.Add(new Claim(JwtClaimTypes.Gender, user.Gender ?? ""));
        claims.AddRange(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));
        // Specific claim
        claims.Add(new Claim("avatar_url", user.AvatarUrl ?? ""));
        claims.Add(new Claim("background_url", user.BackgroundUrl ?? ""));
        claims.Add(new Claim("dob", user.Dob.ToString() ?? ""));
        //claims.Add(new Claim("introduction", user.Introduction));
        //claims.Add(new Claim("active", user?.Active.ToString())); // claim only accept string value
        // Return the claim to client
        context.IssuedClaims = [.. claims];
    }
    private void RemoveClaim(List<Claim> claims, string claimName)
    {
        var existingClaim = claims.FirstOrDefault(c => c.Type == claimName);
        if (existingClaim != null)
        {
            claims.Remove(existingClaim);
        }
    }
    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        context.IsActive = user?.Active ?? false;
    }
}
