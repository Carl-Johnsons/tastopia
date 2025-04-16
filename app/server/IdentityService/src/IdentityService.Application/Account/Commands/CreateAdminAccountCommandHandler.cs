using Contract.Constants;
using Contract.Event.TrackingEvent;
using Contract.Utilities;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using UploadFileProto;
using UserProto;
using static UploadFileProto.GrpcUploadFile;

namespace IdentityService.Application.Account.Commands;

public record CreateAdminAccountCommand : IRequest<Result>
{
    public Guid CurrentAccountId { get; set; }
    public string Name { get; set; } = null!;
    public string Gmail { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public DateTime Dob { get; set; }
    public string Address { get; set; } = null!;
    public IFormFile AvatarFile { get; set; } = null!;
}

public class CreateAdminAccountCommandHandler : IRequestHandler<CreateAdminAccountCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly GrpcUploadFile.GrpcUploadFileClient _grpcUploadFileClient;
    private readonly IServiceBus _serviceBus;

    public CreateAdminAccountCommandHandler(UserManager<ApplicationAccount> userManager,
                                            GrpcUser.GrpcUserClient grpcUserClient,
                                            GrpcUploadFileClient grpcUploadFileClient,
                                            IServiceBus serviceBus)
    {
        _userManager = userManager;
        _grpcUserClient = grpcUserClient;
        _grpcUploadFileClient = grpcUploadFileClient;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(CreateAdminAccountCommand request,
                               CancellationToken cancellationToken)
    {
        var account = _userManager.Users.SingleOrDefault(u => u.Email == request.Gmail);
        if (account != null)
        {
            return Result.Failure(AccountError.EmailAlreadyExisted);
        }

        account = _userManager.Users.SingleOrDefault(u => u.PhoneNumber == request.Phone);
        if (account != null)
        {
            return Result.Failure(AccountError.PhoneAlreadyExisted);
        }

        string username = GenerateUsername(request.Name);

        var usernameSet = _userManager.Users.Select(account => account.UserName).ToHashSet();
        do
        {
            username = GenerateUsername(request.Name);
        } while (usernameSet.Contains(username));

        var acc = new ApplicationAccount
        {
            Email = request.Gmail,
            PhoneNumber = request.Phone,
            UserName = username,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var generatePassword = GeneratePassword(8);

        var result = await _userManager.CreateAsync(acc, generatePassword);
        await _userManager.AddToRoleAsync(acc, Roles.Code.ADMIN.ToString());

        if (!result.Succeeded)
        {
            return Result.Failure(AccountError.CreateAccountFailed);
        }

        var streams = await FileUtility.ConvertIFormFileToGrpcFileStreamDTOAsync([request.AvatarFile]);
        var requestList = new RepeatedField<GrpcFileStreamDTO>();
        requestList.AddRange(streams);
        var uploadResponse = await _grpcUploadFileClient.UploadMultipleImageAsync(new GrpcUploadMultipleImageRequest
        {
            FileStreams = { requestList }
        });
        if (uploadResponse.Files.Count != requestList.Count)
        {
            await _userManager.DeleteAsync(acc);
            return Result.Failure(AccountError.CreateAccountFailed);
        }
        var avatarUrl = uploadResponse.Files[0].Url;

        await _grpcUserClient.CreateAdminUserAsync(new GrpcCreateAdminRequest
        {
            AccountId = acc.Id,
            AccountUsername = username,
            Address = request.Address,
            AvatarURL = avatarUrl,
            DisplayName = request.Name,
            Dob = request.Dob.ToTimestamp(),
            Gender = request.Gender
        }, cancellationToken: cancellationToken);

        await _serviceBus.Publish(new AddActivityLogEvent
        {
            AccountId = request.CurrentAccountId,
            EntityId = Guid.Parse(acc.Id),
            ActivityType = ActivityType.CREATE,
            EntityType = ActivityEntityType.ADMIN,
        });

        await _serviceBus.Publish(new UserSendOTPEvent
        {
            AccountId = Guid.Parse(acc.Id),
            Identifier = acc.Email,
            Method = AccountMethod.Email,
            OTP = generatePassword,
            OTPMethod = OTPMethod.AdminAccountCreated
        });

        return Result.Success();
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

    private string GeneratePassword(int length)
    {
        if (length < 6)
            throw new ArgumentException("Password length must be at least 6 characters.");

        Random random = new Random();
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string special = "!@#$%^&*()-_=+[]{}|;:,.<>?";
        const string allChars = upper + lower + digits + special;

        char[] password = new char[length];
        password[0] = upper[random.Next(upper.Length)];
        password[1] = lower[random.Next(lower.Length)];
        password[2] = digits[random.Next(digits.Length)];
        password[3] = special[random.Next(special.Length)];

        for (int i = 4; i < length; i++)
        {
            password[i] = allChars[random.Next(allChars.Length)];
        }

        return new string(password.OrderBy(_ => random.Next()).ToArray());
    }
}
