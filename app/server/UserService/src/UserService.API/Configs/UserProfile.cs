using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.WellKnownTypes;
using UserProto;
using UserService.Domain.Entities;
using UserService.Domain.Responses;

namespace UserService.API.Configs;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDetailsDTO, GetUserDetailsResponse>().ReverseMap();
        CreateMap<User, SimpleUser>().ReverseMap();
        CreateMap<GetUserDetailsResponse, GrpcUserDetailDTO>()
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
        CreateMap<GetSimpleUsersDTO, GrpcGetSimpleUsersDTO>()
                    .ForMember(dest => dest.Users,
                        opt => opt.MapFrom(src => src.Users.ToDictionary(
                            user => user.Key,
                            user => new CommonProto.GrpcSimpleUser
                            {
                                AccountId = user.Value.AccountId.ToString(),
                                AvtUrl = user.Value.AvtUrl,
                                DisplayName = user.Value.DisplayName
                            }))).ReverseMap();
    }
}
