
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.RegularExpressions;
using UserProto;

namespace IdentityService.Application.Account.Commands;

public record LoginWithGoogleCommand : IRequest<Result<ApplicationAccount>>
{
    public string Provider { get; set; } = null!;
    public string ProviderUserId { get; set; } = null!;
    // AccessToken return from identity provider like Google
    public string AccessToken { get; set; } = null!;
    public IEnumerable<Claim> Claims { get; set; } = [];
}


public class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommand, Result<ApplicationAccount>>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly ILogger<LoginWithGoogleCommandHandler> _logger;

    public LoginWithGoogleCommandHandler(UserManager<ApplicationAccount> userManager, GrpcUser.GrpcUserClient grpcUserClient, ILogger<LoginWithGoogleCommandHandler> logger)
    {
        _userManager = userManager;
        _grpcUserClient = grpcUserClient;
        _logger = logger;
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
            user.UserName = GenerateUsername(GetLocalPart(email));
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
        if (string.IsNullOrEmpty(user.UserName))
        {
            user.UserName = GenerateUsername(fullName);
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

    private string GetLocalPart(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);

            return mailAddress.User;
        }
        catch (FormatException)
        {
            // Handle invalid email format
            return "";
        }
    }

    private string GenerateUsername(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be null or empty.");

        // Remove spaces and convert to lowercase
        var baseUsername = Regex.Replace(fullName.ToLower(), @"\s+", "");

        // Remove non-alphanumeric characters
        baseUsername = Regex.Replace(baseUsername, @"[^a-z0-9]", "");

        var random = new Random();
        var randomNumber = random.Next(1000, 9999);

        return $"{baseUsername}{randomNumber}";
    }

    // TODO: This function is BROKEN, right now the api only return emailAddresses not phoneNumber, need further investigation
    private async Task<string?> GetGooglePhoneNumberAsync(string accessToken)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var apiKey = DotNetEnv.Env.GetString("GOOGLE_API_KEY", "Not found");

        string url = $"https://people.googleapis.com/v1/people/me?personFields=phoneNumbers,emailAddresses&key={apiKey}";

        var response = await client.GetStringAsync(url);
        var userInfo = JObject.Parse(response);

        // Extract phone number if available
        var phoneNumber = userInfo["phoneNumbers"]?[0]?["value"]?.ToString();

        return phoneNumber;
    }
}
