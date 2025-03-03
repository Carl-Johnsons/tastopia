
using AutoMapper;
using Contract.Constants;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using IdentityService.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserProto;

namespace IdentityService.Application.Account.Queries;

public record FindAccountQuery : IRequest<Result<SimpleUserResponse?>>
{
    public string Identifier { get; set; } = null!;
    public AccountMethod Method { get; set; }
}

public class FindAccountQueryHandler : IRequestHandler<FindAccountQuery, Result<SimpleUserResponse?>>
{
    private readonly UserManager<ApplicationAccount> _userManager;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IMapper _mapper;

    public FindAccountQueryHandler(UserManager<ApplicationAccount> userManager, GrpcUser.GrpcUserClient grpcUserClient, IMapper mapper)
    {
        _userManager = userManager;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
    }

    public async Task<Result<SimpleUserResponse?>> Handle(FindAccountQuery request, CancellationToken cancellationToken)
    {
        ApplicationAccount? acc;
        switch (request.Method)
        {
            case AccountMethod.Email:
                acc = await _userManager.FindByEmailAsync(request.Identifier);
                break;
            case AccountMethod.Phone:
                acc = await _userManager.Users.SingleOrDefaultAsync(a => a.PhoneNumber == request.Identifier);
                break;
            default:
                return Result<SimpleUserResponse>.Failure(AccountError.InvalidAccountMethod);
        }

        if (acc == null)
        {
            return Result<SimpleUserResponse>.Failure(AccountError.NotFound);
        }
        List<string> accountList = [acc.Id];


        var response = _grpcUserClient.GetSimpleUser(new GrpcGetSimpleUsersRequest
        {
            AccountId = { _mapper.Map<RepeatedField<string>>(accountList) }
        });

        var mapUsers = new Dictionary<Guid, SimpleUser>();
        foreach (var (key, value) in response.Users)
        {
            mapUsers[Guid.Parse(key)] = new SimpleUser
            {
                AccountId = Guid.Parse(value.AccountId),
                AvtUrl = value.AvtUrl,
                DisplayName = value.DisplayName,
            };
        }

        if (response == null || mapUsers[Guid.Parse(acc.Id)] == null)
        {
            return Result<SimpleUserResponse>.Failure(AccountError.NotFound, "Account profile not found");
        }

        var simpleUser = new SimpleUserResponse
        {
            Id = Guid.Parse(acc.Id),
            AvtUrl = mapUsers[Guid.Parse(acc.Id)].AvtUrl,
            DisplayName = mapUsers[Guid.Parse(acc.Id)].DisplayName
        };

        return Result<SimpleUserResponse?>.Success(simpleUser);
    }
}
