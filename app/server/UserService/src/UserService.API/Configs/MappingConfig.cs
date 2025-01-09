using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.WellKnownTypes;
using UserProto;
using UserService.Domain.Entities;
using UserService.Domain.Responses;

namespace UserService.API.Configs;

public class MappingConfig
{

    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            //config.CreateMap<Us, AccountDTO>().ReverseMap();
            config.CreateMap<UserDetailsDTO, GetUserDetailsResponse>().ReverseMap();
            config.CreateMap<User, SimpleUser>().ReverseMap();

            // Map object to grpc object
            config.CreateMap<GetUserDetailsResponse, GrpcUserDetailDTO>()
                   .ForMember(dest => dest.Dob,
                        opt => opt.MapFrom(src => src.Dob.HasValue ?
                                           Timestamp.FromDateTime(((DateTime)src.Dob).ToUniversalTime())
                                           : null))
                   .ForMember(dest => dest.AccountId,
                        opt => opt.MapFrom(src => src.AccountId.ToString()))
                   .ReverseMap()
                   .ForMember(dest => dest.Dob,
                        opt => opt.MapFrom(src => src.Dob.ToDateTime()))
                   .ForMember(dest => dest.AccountId,
                        opt => opt.MapFrom(src => Guid.Parse(src.AccountId)));


            config.CreateMap<GetSimpleUsersDTO, GrpcGetSimpleUsersDTO>()
                    .ForMember(dest => dest.Users,
                        opt => opt.MapFrom(src => src.Users.ToDictionary(
                            user => user.Key,
                            user => new GrpcSimpleUser
                            {
                                AccountId = user.Value.AccountId.ToString(),
                                AvtUrl = user.Value.AvtUrl,
                                DisplayName = user.Value.DisplayName
                            }))).ReverseMap();
        });


        return mappingConfig;
    }
}
