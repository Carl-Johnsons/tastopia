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

public record UpdateAdminAccountCommand : IRequest<Result>
{
    public Guid CurrentAccountId { get; set; }
    public Guid AccountId { get; set; }
    public string? Username { get; set; } = null!;
    public string? Name { get; set; } = null!;
    public string? Gmail { get; set; } = null!;
    public string? Phone { get; set; } = null!;
    public string? Gender { get; set; } = null!;
    public DateTime? Dob { get; set; }
    public string? Address { get; set; } = null!;
    public IFormFile? AvatarFile { get; set; } = null!;
}

public class UpdateAdminAccountCommandHandler : IRequestHandler<UpdateAdminAccountCommand, Result>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly GrpcUploadFile.GrpcUploadFileClient _grpcUploadFileClient;
    private readonly IServiceBus _serviceBus;

    public UpdateAdminAccountCommandHandler(UserManager<ApplicationAccount> userManager,
                                            GrpcUser.GrpcUserClient grpcUserClient,
                                            GrpcUploadFileClient grpcUploadFileClient,
                                            IServiceBus serviceBus)
    {
        _userManager = userManager;
        _grpcUserClient = grpcUserClient;
        _grpcUploadFileClient = grpcUploadFileClient;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(UpdateAdminAccountCommand request,
                               CancellationToken cancellationToken)
    {
        var account = _userManager.Users.SingleOrDefault(u => u.Id == request.AccountId.ToString());
        var grpcUserRequest = new GrpcUpdateAdminRequest
        {
            AccountId = request.AccountId.ToString(),
            AccountUsername = "",
            Address = request.Address ?? "",
            AvatarURL = "",
            DisplayName = "",
            Gender = "",
            Dob = DateTime.UtcNow.ToTimestamp(),
            IsDoBUpdated = false,
        };
        if (account == null)
        {
            return Result.Failure(AccountError.NotFound);
        }

        if (request.Username != null)
        {
            if (_userManager.Users.Any(u => u.UserName == request.Username && u.Id != request.AccountId.ToString()))
            {
                return Result.Failure(AccountError.UsernameAlreadyExisted);
            }
            account.UserName = request.Username;
            grpcUserRequest.AccountUsername = request.Username;
        }

        if (request.Gmail != null)
        {
            if (_userManager.Users.Any(u => u.Email == request.Gmail && u.Id != request.AccountId.ToString()))
            {
                return Result.Failure(AccountError.EmailAlreadyExisted);
            }

            account.Email = request.Gmail;
            account.EmailOTP = null;
            account.EmailConfirmed = false;
            account.EmailOTPCreated = null;
            account.EmailOTPExpiry = null;
        }

        if (request.Phone != null)
        {
            if (_userManager.Users.Any(u => u.PhoneNumber == request.Phone && u.Id != request.AccountId.ToString()))
            {
                return Result.Failure(AccountError.PhoneAlreadyExisted);
            }

            account.PhoneNumber = request.Phone;
            account.PhoneOTP = null;
            account.PhoneNumberConfirmed = false;
            account.PhoneOTPCreated = null;
            account.PhoneOTPExpiry = null;
        }

        if (request.AvatarFile != null)
        {
            var streams = await FileUtility.ConvertIFormFileToGrpcFileStreamDTOAsync([request.AvatarFile]);
            var requestList = new RepeatedField<GrpcFileStreamDTO>();
            requestList.AddRange(streams);
            var uploadResponse = await _grpcUploadFileClient.UploadMultipleImageAsync(new GrpcUploadMultipleImageRequest
            {
                FileStreams = { requestList }
            });
            if (uploadResponse.Files.Count != requestList.Count)
            {
                return Result.Failure(AccountError.UpdateAccountFailed, "Upload avatar failed");
            }

            var avatarUrl = uploadResponse.Files[0].Url;
            grpcUserRequest.AvatarURL = avatarUrl;
        }

        if (request.Name != null)
        {
            grpcUserRequest.DisplayName = request.Name;
        }

        if (request.Gender != null)
        {
            grpcUserRequest.Gender = request.Gender;
        }

        if (request.Dob != null)
        {
            grpcUserRequest.Dob = ((DateTime)request.Dob).ToTimestamp();
            grpcUserRequest.IsDoBUpdated = true;
        }

        account.UpdatedAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(account);
        await _grpcUserClient.UpdateAdminUserAsync(grpcUserRequest, cancellationToken: cancellationToken);

        await _serviceBus.Publish(new AddActivityLogEvent
        {
            AccountId = request.CurrentAccountId,
            EntityId = Guid.Parse(account.Id),
            ActivityType = ActivityType.UPDATE,
            EntityType = ActivityEntityType.USER,
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
}
