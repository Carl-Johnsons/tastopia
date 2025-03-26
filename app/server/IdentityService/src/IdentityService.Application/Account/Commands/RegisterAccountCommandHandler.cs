using Contract.Constants;
using IdentityModel.Client;
using IdentityService.Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using UserProto;
namespace IdentityService.Application.Account.Commands;
public record RegisterAccountCommand : IRequest<Result<TokenResponse?>>
{
    [Required]
    public string Identifier { get; set; } = null!;
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public AccountMethod Method { get; set; }
}
public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, Result<TokenResponse?>>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly IServiceBus _serviceBus;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IConsulRegistryService _consulRegistryService;
    private readonly ILogger<RegisterAccountCommandHandler> _logger;
    public RegisterAccountCommandHandler(UserManager<ApplicationAccount> userManager, IServiceBus serviceBus, GrpcUser.GrpcUserClient grpcUserClient, IConsulRegistryService consulRegistryService, ILogger<RegisterAccountCommandHandler> logger)
    {
        _userManager = userManager;
        _serviceBus = serviceBus;
        _grpcUserClient = grpcUserClient;
        _consulRegistryService = consulRegistryService;
        _logger = logger;
    }
    public async Task<Result<TokenResponse?>> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        switch (request.Method)
        {
            case AccountMethod.Email:
                return await RegisterByEmail(request, cancellationToken);
            case AccountMethod.Phone:
                return await RegisterByPhone(request, cancellationToken);
            default:
                return Result<TokenResponse?>.Failure(AccountError.CreateAccountFailed, "Wrong account method");
        }
    }

    private async Task<Result<TokenResponse?>> RegisterByEmail(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        var account = _userManager.Users.SingleOrDefault(u => u.Email == request.Identifier);
        if (account != null)
        {
            return Result<TokenResponse>.Failure(AccountError.EmailAlreadyExisted);
        }

        var OTP = OTPUtility.GenerateNumericOTP();

        // Generate unique username
        var username = GenerateUsername(request.FullName);

        var acc = new ApplicationAccount
        {
            UserName = username,
            Email = request.Identifier,
            EmailOTP = OTP,
            EmailOTPCreated = DateTime.UtcNow,
            EmailOTPExpiry = DateTime.UtcNow.AddMinutes(5),
        };

        var result = await _userManager.CreateAsync(acc, request.Password);
        if (!result.Succeeded)
        {
            return Result<TokenResponse>.Failure(AccountError.CreateAccountFailed);
        }

        await _grpcUserClient.CreateUserAsync(new GrpcCreateUserRequest
        {
            AccountId = acc.Id,
            AccountUsername = username,
            FullName = request.FullName,
            Avatar = ""
        });

        await _serviceBus.Publish(new UserRegisterEvent
        {
            AccountId = Guid.Parse(acc.Id),
            Identifier = request.Identifier,
            Method = AccountMethod.Email,
            OTP = OTP,
            FullName = request.FullName,
            AccountUsername = username,
        });

        var tokenIssued = await RequestTokenAsync(username, request.Password);
        return Result<TokenResponse?>.Success(tokenIssued);
    }

    private async Task<Result<TokenResponse?>> RegisterByPhone(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        var account = _userManager.Users.SingleOrDefault(u => u.PhoneNumber == request.Identifier);
        if (account != null)
        {
            return Result<TokenResponse>.Failure(AccountError.PhoneAlreadyExisted);
        }

        var OTP = OTPUtility.GenerateNumericOTP();

        // Generate unique username
        var username = GenerateUsername(request.FullName);

        var acc = new ApplicationAccount
        {
            UserName = username,
            PhoneNumber = request.Identifier,
            PhoneOTP = OTP,
            PhoneOTPCreated = DateTime.UtcNow,
            PhoneOTPExpiry = DateTime.UtcNow.AddMinutes(5),
        };

        var result = await _userManager.CreateAsync(acc, request.Password);
        if (!result.Succeeded)
        {
            return Result<TokenResponse>.Failure(AccountError.CreateAccountFailed);
        }

        await _serviceBus.Publish(new UserRegisterEvent
        {
            AccountId = Guid.Parse(acc.Id),
            Identifier = request.Identifier,
            Method = AccountMethod.Phone,
            OTP = OTP,
            FullName = request.FullName,
            AccountUsername = username
        });

        var tokenIssued = await RequestTokenAsync(username, request.Password);
        return Result<TokenResponse?>.Success(tokenIssued);
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

    // User name can be username or email or password
    private async Task<TokenResponse> RequestTokenAsync(string username, string password)
    {
        var client = new HttpClient();

        var uri = _consulRegistryService.GetServiceUri(DotNetEnv.Env.GetString("CONSUL_IDENTITY", "Not found"));

        _logger.LogInformation(uri!.ToString());
        var discovery = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = uri!.ToString(),
            Policy = new DiscoveryPolicy
            {
                RequireHttps = false
            }
        });

        if (discovery.IsError)
        {
            throw new Exception(discovery.Error);
        }

        // Request token
        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = discovery.TokenEndpoint,
            ClientId = "react.native",
            UserName = username,
            Password = password,
            Scope = "openid profile phone email offline_access IdentityServerApi"
        });

        if (tokenResponse.IsError)
        {
            throw new Exception(tokenResponse.Error);
        }

        return tokenResponse;
    }
}
