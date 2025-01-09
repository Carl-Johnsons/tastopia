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
            config.CreateMap<UserDetailsDTO, GrpcUserDetailDTO>()
                   .ForMember(dest => dest.Dob,
                        opt => opt.MapFrom(src => src.Dob.HasValue ?
                                           Timestamp.FromDateTime(((DateTime)src.Dob).ToUniversalTime())
                                           : null))
                   .ReverseMap()
                   .ForMember(dest => dest.Dob,
                        opt => opt.MapFrom(src => src.Dob.ToDateTime()));


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
