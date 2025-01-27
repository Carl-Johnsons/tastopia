
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;
using UserProto;

namespace IdentityService.Application.Account.Commands;

public record LoginWithGoogleCommand : IRequest<Result<ApplicationAccount>>
{
    public string Provider { get; set; } = null!;
    public string ProviderUserId { get; set; } = null!;
    public IEnumerable<Claim> Claims { get; set; } = [];
}


public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommand, Result<ApplicationAccount>>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public LoginWithGoogleCommandHandler(UserManager<ApplicationAccount> userManager, GrpcUser.GrpcUserClient grpcUserClient)
    {
        _userManager = userManager;
        _grpcUserClient = grpcUserClient;
    }

    async Task<Result<ApplicationAccount>> IRequestHandler<LoginWithGoogleCommand, Result<ApplicationAccount>>.Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
    {
        var provider = request.Provider;
        var providerUserId = request.ProviderUserId;
        var claims = request.Claims;

        var sub = Guid.NewGuid().ToString();
        string fullName = "";
        string avatar = "";

        var user = new ApplicationAccount
        {
            Id = sub,
            UserName = sub, // don't need a username, since the user will be using an external provider to login
            EmailConfirmed = true
        };

        var simplifiedClaims = claims.Select(c => new
        {
            c.Type,
            c.Value,
            c.Issuer
        });

        Console.WriteLine(JsonConvert.SerializeObject(simplifiedClaims, Formatting.Indented));

        // email
        var email = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Email)?.Value ??
                    claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (email != null)
        {
            user.Email = email;
        }

        // create a list of claims that we want to transfer into our store
        var filtered = new List<Claim>();

        // user's display name
        var name = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value ??
                   claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        if (name != null)
        {
            fullName = name;
        }
        else
        {
            var first = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value ??
                        claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            var last = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value ??
                       claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
            if (first != null && last != null)
            {
                fullName = first + " " + last;
            }
            else if (first != null)
            {
                fullName = first;
            }
            else if (last != null)
            {
                fullName = last;
            }
        }
        filtered.Add(new Claim(JwtClaimTypes.Name, fullName));

        avatar = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Picture)?.Value ?? "";

        var identityResult = await _userManager.CreateAsync(user);
        if (!identityResult.Succeeded) throw new InvalidOperationException(identityResult.Errors.First().Description);

        if (filtered.Count != 0)
        {
            identityResult = await _userManager.AddClaimsAsync(user, filtered);
            if (!identityResult.Succeeded) throw new InvalidOperationException(identityResult.Errors.First().Description);
        }

        identityResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerUserId, provider));
        if (!identityResult.Succeeded) throw new InvalidOperationException(identityResult.Errors.First().Description);

        // Add user to user service
        await _grpcUserClient.CreateUserAsync(new GrpcCreateUserRequest
        {
            AccountId = sub,
            AccountUsername = user.UserName,
            FullName = fullName,
            Avatar = avatar
        });

        return Result<ApplicationAccount>.Success(user);
    }
}
