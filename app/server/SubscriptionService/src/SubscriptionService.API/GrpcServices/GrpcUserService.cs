using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Newtonsoft.Json;
using UserProto;

namespace SubscriptionService.API.GrpcServices;

public class GrpcUserService : GrpcUser.GrpcUserBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<GrpcUserService> _logger;

    public GrpcUserService(IMapper mapper, ILogger<GrpcUserService> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

}
