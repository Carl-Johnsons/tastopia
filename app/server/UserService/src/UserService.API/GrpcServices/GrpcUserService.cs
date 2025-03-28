using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserProto;
using UserService.Application.Settings.Queries;
using UserService.Application.UserReports.Queries;
using UserService.Application.Users.Commands;
using UserService.Application.Users.Queries;
using UserService.Domain.Entities;
using UserService.Domain.Responses;

namespace UserService.API.GrpcServices;

public class GrpcUserService : GrpcUser.GrpcUserBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ILogger<GrpcUserService> _logger;

    public GrpcUserService(ISender sender, IMapper mapper, ILogger<GrpcUserService> logger)
    {
        _sender = sender;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<GrpcGetSimpleUsersDTO> GetSimpleUser(GrpcGetSimpleUsersRequest request, ServerCallContext context)
    {
        if (request.AccountId == null || request.AccountId.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "AccountId must not be null or empty."));
        }

        var accountIdSets = request.AccountId.Select(Guid.Parse).ToHashSet();

        var response = await _sender.Send(new GetSimpleUsersQuery
        {
            AccountIds = accountIdSets,
        });

        response.ThrowIfFailure();

        var users = response.Value!;

        var mapUser = users.Select(u => new SimpleUser
        {
            AccountId = u.AccountId,
            AvtUrl = u.AvatarUrl,
            DisplayName = u.DisplayName,
            AccountUsername = u.AccountUsername,
        }).ToDictionary(u => u.AccountId);


        var mapField = new MapField<string, CommonProto.GrpcSimpleUser>();
        foreach (var (key, value) in mapUser)
        {
            mapField[key.ToString()] = new CommonProto.GrpcSimpleUser
            {
                AccountId = value.AccountId.ToString(),
                AvtUrl = value.AvtUrl,
                DisplayName = value.DisplayName,
                AccountUsername = value.AccountUsername,
            };
        }

        var grpcResult = new GrpcGetSimpleUsersDTO
        {
            Users = { mapField }
        };
        _logger.LogInformation(JsonConvert.SerializeObject(grpcResult, Formatting.Indented));
        return grpcResult;
    }

    public override async Task<GrpcUserDetailDTO> GetUserDetail(GrpcAccountIdRequest request, ServerCallContext context)
    {
        var response = await _sender.Send(new GetUserDetailsQuery
        {
            AccountId = Guid.Parse(request.AccountId),
        });
        response.ThrowIfFailure();

        GetUserDetailsResponse user = response.Value!;

        // TODO: Change this to automapper
        var grpcResponse = new GrpcUserDetailDTO
        {
            AccountEmail = user.AccountEmail,
            AccountId = user.AccountId.ToString(),
            AccountPhoneNumber = user.AccountPhoneNumber,
            AccountUsername = user.AccountUsername,
            Address = user.Address,
            AvatarUrl = user.AvatarUrl,
            BackgroundUrl = user.BackgroundUrl,
            Bio = user.Bio,
            DisplayName = user.DisplayName,
            Dob = user.Dob.HasValue ? Timestamp.FromDateTime(((DateTime)user.Dob).ToUniversalTime()) : null,
            Gender = user.Gender,
            IsAccountActive = user.IsAccountActive,
            IsAdmin = user.IsAdmin,
            TotalFollower = user.TotalFollower ?? 0,
            TotalFollowing = user.TotalFollowing ?? 0,
            TotalRecipe = user.TotalRecipe ?? 0,
        };
        return grpcResponse;
    }
    public override async Task<GrpcEmpty> CreateUser(GrpcCreateUserRequest request, ServerCallContext context)
    {
        var accountId = Guid.Parse(request.AccountId);
        var defaultAvatar = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png";
        var defaultBackground = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png";
        var fullName = request.FullName;
        var username = request.AccountUsername;
        var avatar = string.IsNullOrEmpty(request.Avatar) ? defaultAvatar : request.Avatar;

        var user = new User
        {
            AccountId = accountId,
            AvatarUrl = avatar,
            BackgroundUrl = defaultBackground,
            DisplayName = fullName,
            IsAccountActive = true,
            AccountUsername = username,
            IsAdmin = false
        };

        var response = await _sender.Send(new CreateUserCommand
        {
            User = user,
        });

        response.ThrowIfFailure();

        _logger.LogInformation("Grpc create user successfully");
        _logger.LogInformation(JsonConvert.SerializeObject(user, Formatting.Indented));
        return new GrpcEmpty();
    }
    public override async Task<GrpcListAccountIds> SearchUser(GrpcSearchUserRequest request, ServerCallContext context)
    {
        var keyword = request.Keyword;
        var response = await _sender.Send(new SearchSimpleUserQuery
        {
            Keyword = keyword,
        });
        response.ThrowIfFailure();

        var result = new GrpcListAccountIds
        {
            AccountIds = { response.Value!.Select(id => id.ToString()) }
        };
        return result;
    }

    public override async Task<GrpcUserSetting> GetUserSetting(GrpcGetUserSettingRequest request, ServerCallContext context)
    {
        if (request.AccountId == null || request.AccountId.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "AccountId must not be null or empty."));
        }

        var accountIdSets = request.AccountId.Select(Guid.Parse).ToHashSet();

        var response = await _sender.Send(new GetMultipleUserSettingQuery
        {
            AccountIdSet = accountIdSets,
        });

        response.ThrowIfFailure();

        var userSettingMap = response.Value!;

        var mapField = new MapField<string, GrpcSettingCollection>();
        foreach (var (key, value) in userSettingMap)
        {
            var repeatedField = new RepeatedField<GrpcSetting>();
            foreach (var v in value)
            {
                repeatedField.Add(new GrpcSetting
                {
                    DefaultValue = v.Setting.DefaultValue,
                    SettingCode = v.Setting.Code,
                    SettingId = v.SettingId.ToString(),
                    SettingValue = v.SettingValue,
                    SettingType = v.Setting.DataType.ToString()
                });
            }

            var keyStr = key.ToString();
            if (!mapField.ContainsKey(keyStr))
            {
                mapField.Add(keyStr, new GrpcSettingCollection());
            }

            mapField[keyStr].Settings.AddRange(repeatedField);
        }

        var grpcResult = new GrpcUserSetting
        {
            SettingMap = { mapField }
        };
        _logger.LogInformation(JsonConvert.SerializeObject(grpcResult, Formatting.Indented));

        return grpcResult;
    }

    public override async Task<GrpcListAccountIds> GetUserFollower(GrpcAccountIdRequest request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.AccountId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "AccountId must not be null or empty."));
        }

        var accountId = Guid.Parse(request.AccountId);

        var response = await _sender.Send(new GetUserFollowerIdsQuery
        {
            AccountId = accountId,
        });
        response.ThrowIfFailure();

        var result = new GrpcListAccountIds
        {
            AccountIds = { response.Value!.Select(id => id.ToString()) }
        };
        return result;


    }
    public override async Task<GrpcListAccountIds> GetUserFollowing(GrpcAccountIdRequest request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.AccountId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "AccountId must not be null or empty."));
        }

        var accountId = Guid.Parse(request.AccountId);

        var response = await _sender.Send(new GetUserFollowingIdsQuery
        {
            AccountId = accountId,
        });
        response.ThrowIfFailure();

        var result = new GrpcListAccountIds
        {
            AccountIds = { response.Value!.Select(id => id.ToString()) }
        };
        return result;
    }

    public override async Task<GrpcSimpleUserReport> GetSimpleUserReport(GrpcGetUserReportRequest request, ServerCallContext context)
    {
        var hashSet = request.ReportIds.Select(Guid.Parse).ToHashSet();

        var result = await _sender.Send(new GetUserReportQueryByHashSet
        {
            Lang = request.Lang,
            ReportIds = hashSet
        });

        result.ThrowIfFailure();


        var mapField = new MapField<string, GrpcUserReport>();
        foreach (var (k, v) in result.Value!)
        {
            RepeatedField<string> reasonRepeatedField = [.. v.Report.Reasons];

            mapField.Add(k.ToString(), new GrpcUserReport
            {
                Report = new CommonProto.GrpcSimpleReport
                {
                    Id = v.Report.Id.ToString(),
                    AdditionalDetail = v.Report.AdditionalDetail,
                    CreatedAt = v.Report.CreatedAt.ToTimestamp(),
                    Reasons = { reasonRepeatedField },
                    ReporterAccountId = v.Report.ReporterAccountId.ToString(),
                    Status = v.Report.Status
                },
                User = new CommonProto.GrpcSimpleUser
                {
                    AccountId = v.User.AccountId.ToString(),
                    AccountUsername = v.User.AccountUsername,
                    AvtUrl = v.User.AvtUrl,
                    DisplayName = v.User.DisplayName
                },
                Reporter = new CommonProto.GrpcSimpleUser
                {
                    AccountId = v.Reporter.AccountId.ToString(),
                    AccountUsername = v.Reporter.AccountUsername,
                    AvtUrl = v.Reporter.AvtUrl,
                    DisplayName = v.Reporter.DisplayName
                }
            });
        }

        return new GrpcSimpleUserReport
        {
            Reports = { mapField }
        };
    }

    public override async Task<GrpcEmpty> CreateAdminUser(GrpcCreateAdminRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(new CreateUserCommand
        {
            User = new User
            {
                AccountId = Guid.Parse(request.AccountId),
                AccountUsername = request.AccountUsername,
                Address = request.Address,
                AvatarUrl = request.AvatarURL,
                DisplayName = request.DisplayName,
                Dob = request.Dob.ToDateTime(),
                Gender = request.Gender,
                IsAdmin = true,
                IsAccountActive = true,
                Bio = "",
                BackgroundUrl = "",
                TotalFollower = 0,
                TotalFollowing = 0,
                TotalRecipe = 0
            }
        });

        result.ThrowIfFailure();

        return new GrpcEmpty();
    }

    public override async Task<GrpcEmpty> UpdateAdminUser(GrpcUpdateAdminRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(new UpdateGprcUserCommand
        {
            AccountId = Guid.Parse(request.AccountId),
            Username = request.AccountUsername,
            Address = request.Address,
            AvatarUrl = request.AvatarURL,
            DisplayName = request.DisplayName,
            Dob = request.Dob.ToDateTime(),
            Gender = request.Gender,
            IsDobUpdate = request.IsDoBUpdated
        });

        result.ThrowIfFailure();

        return new GrpcEmpty();
    }
}
