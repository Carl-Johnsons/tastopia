using AutoMapper;
using Contract.DTOs.UserDTO;
using Grpc.Core;
using Newtonsoft.Json;
using UserProto;
using UserService.Application.Users.Commands;

namespace UserService.API.GrpcServices;

public class UserService : GrpcUser.GrpcUserBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserService(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override async Task<GrpcGetSimpleUsersDTO> GetSimpleUser(GrpcGetSimpleUsersRequest request, ServerCallContext context)
    {
        Console.WriteLine("=========================== Call simple user ========================");
        if (request.AccountId == null || request.AccountId.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "AccountId must not be null or empty."));
        }

        var accountIdSets = request.AccountId.Select(Guid.Parse).ToHashSet();

        var response = await _sender.Send(new GetSimpleUsersCommand
        {
            AccountIds = accountIdSets,
        });

        response.ThrowIfFailure();

        var users = response.Value!;

        var mapUser = users.Select(_mapper.Map<SimpleUser>).ToDictionary(u => u.AccountId);

        var result = new GetSimpleUsersDTO
        {
            Users = mapUser,
        };

        var grpcResult = _mapper.Map<GrpcGetSimpleUsersDTO>(result);

        Console.WriteLine(JsonConvert.SerializeObject(grpcResult));
        Console.WriteLine("=========================== Done ========================");

        return grpcResult;
    }
}
