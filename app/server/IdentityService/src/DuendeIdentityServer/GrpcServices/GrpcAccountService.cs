using AccountProto;
using Grpc.Core;
using IdentityService.Application.Account.Commands;
using IdentityService.Application.Account.Queries;
using Newtonsoft.Json;

namespace DuendeIdentityServer.GrpcServices;

public class GrpcAccountService : GrpcAccount.GrpcAccountBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ILogger<GrpcAccountService> _logger;
    public GrpcAccountService(ISender sender, IMapper mapper, ILogger<GrpcAccountService> logger)
    {
        _sender = sender;
        _mapper = mapper;
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
}
