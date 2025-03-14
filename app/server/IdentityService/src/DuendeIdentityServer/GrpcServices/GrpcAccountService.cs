using AccountProto;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using IdentityService.Application.Account.Commands;
using IdentityService.Application.Account.Queries;
using Newtonsoft.Json;

namespace DuendeIdentityServer.GrpcServices;

public class GrpcAccountService : GrpcAccount.GrpcAccountBase
{
    private readonly ISender _sender;
    private readonly ILogger<GrpcAccountService> _logger;
    public GrpcAccountService(ISender sender, ILogger<GrpcAccountService> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    public override async Task<GrpcAccountDTO> GetAccountDetail(GrpcAccountIdRequest request, ServerCallContext context)
    {
        var accountIdSets = new HashSet<Guid>
        {
            Guid.Parse(request.AccountId)
        };

        var result = await _sender.Send(new GetAccountDetailQuery
        {
            Ids = accountIdSets
        });

        result.ThrowIfFailure();

        var grpcResponse = new GrpcAccountDTO
        {
            Email = result.Value![0].Email ?? "",
            IsActive = result.Value![0].IsActive,
            PhoneNumber = result.Value![0].PhoneNumber ?? "",
            UserName = result.Value![0].UserName,
            CreatedAt = Timestamp.FromDateTime(result.Value![0].CreatedAt.ToUniversalTime()),
            UpdatedAt = Timestamp.FromDateTime(result.Value![0].UpdatedAt.ToUniversalTime()),
        };

        _logger.LogInformation("Grpc get account detail successfully!");
        _logger.LogInformation(JsonConvert.SerializeObject(grpcResponse, Formatting.Indented));

        return grpcResponse;
    }

    public override async Task<GrpcEmpty> UpdateAccount(GrpcUpdateAccountRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(new UpdateAccountCommand
        {
            AccountId = Guid.Parse(request.AccountId),
            UserName = request.UserName
        });
        result.ThrowIfFailure();

        _logger.LogInformation("Grpc update account successfully!");

        return new GrpcEmpty();
    }

    public override async Task<GrpcListSimpleAccountsDTO> GetSimpleAccounts(GrpcAccountIdListRequest request, ServerCallContext context)
    {
        var accountIdSets = new HashSet<Guid>();
        foreach(var id in request.AccountIds)
        {
            accountIdSets.Add(Guid.Parse(id));
        }

        var result = await _sender.Send(new GetAccountDetailQuery
        {
            Ids = accountIdSets
        });

        result.ThrowIfFailure();

        var grpcResponse = new GrpcListSimpleAccountsDTO();
      
        foreach (var acc in result.Value!)
        {
            grpcResponse.Accounts.Add(acc.Id, new GrpcSimpleAccountDTO
            {
                Email = acc.Email,
                PhoneNumber = acc.PhoneNumber
            });
        }
        _logger.LogInformation("Grpc get simple account successfully!");
        _logger.LogInformation(JsonConvert.SerializeObject(grpcResponse, Formatting.Indented));
        return grpcResponse;
    }

    public override async Task<GrpcListAccountIds> SearchAccount(GrpcSearchAccountRequest request, ServerCallContext context)
    {
        var keyword = request.Keyword;
        var response = await _sender.Send(new SearchSimpleAccountQuery
        {
            Keyword = keyword,
        });
        response.ThrowIfFailure();

        var result = new GrpcListAccountIds
        {
            AccountIds = { response.Value}
        };
        return result;
    }
}
