using AccountProto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
using UserService.Domain.Responses;

namespace UserService.Application.Users.Queries;

public class AdminGetUserDetailQuery : IRequest<Result<AdminGetUserDetailResponse?>>
{
    [Required]
    public Guid AccountId { get; init; }

    [Required]
    public Guid CurrentAccountId { get; init; }
}

public class AdminGetUserDetailQueryHandler : IRequestHandler<AdminGetUserDetailQuery, Result<AdminGetUserDetailResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GrpcAccount.GrpcAccountClient _grpcAccountClient;

    public AdminGetUserDetailQueryHandler(IApplicationDbContext context,
        IMapper mapper,
        GrpcAccount.GrpcAccountClient grpcAccountClient)
    {
        _context = context;
        _mapper = mapper;
        _grpcAccountClient = grpcAccountClient;
    }

    public async Task<Result<AdminGetUserDetailResponse?>> Handle(AdminGetUserDetailQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var currentAccountId = request.CurrentAccountId;

        if (accountId == Guid.Empty || currentAccountId == Guid.Empty)
        {
            return Result<AdminGetUserDetailResponse?>.Failure(UserError.NullParameters, "Account or CurrentAccountId Id is null");
        }

        var user = await _context.Users
         .Where(user => user.AccountId == accountId && !user.IsAdmin)
         .FirstOrDefaultAsync();

        if (user == null)
        {
            return Result<AdminGetUserDetailResponse?>.Failure(UserError.NotFound);
        }

        if (user.IsAdmin && user.AccountId != currentAccountId)
        {
            return Result<AdminGetUserDetailResponse?>.Failure(UserError.PermissionDenied, "Cannot view other admin profile.");
        }

        var grpcRequest = new GrpcAccountIdRequest
        {
            AccountId = accountId.ToString()
        };

        var grpcResponse = await _grpcAccountClient.GetAccountDetailAsync(grpcRequest, cancellationToken: cancellationToken);

        if (grpcResponse == null)
        {
            return Result<AdminGetUserDetailResponse>.Failure(UserError.NotFound);
        }

        var result = _mapper.Map<AdminGetUserDetailResponse>(user);
        result.AccountPhoneNumber = grpcResponse.PhoneNumber;
        result.AccountEmail = grpcResponse.Email;
        result.IsCurrentUser = currentAccountId == accountId;
        result.Role = user.IsAdmin ? "Admin" : "User";
        result.ActiveTime = "24h30m";

        var settings = _context.Settings;
        var settingsDictionary = await settings.ToDictionaryAsync(s => s.Id, s => s);

        List<UserSetting> userSettings = await _context.UserSettings.Where(us => us.AccountId == accountId).ToListAsync() ?? [];

        foreach (var userSetting in userSettings)
        {
            settingsDictionary.Remove(userSetting.SettingId);
        }

        foreach (var key in settingsDictionary.Keys)
        {
            userSettings.Add(new UserSetting
            {
                AccountId = accountId,
                SettingId = key,
                SettingValue = settingsDictionary[key].DefaultValue,
                Setting = settingsDictionary[key]
            });
        }

        result.Settings = userSettings;
        return Result<AdminGetUserDetailResponse?>.Success(result);
    }
}
