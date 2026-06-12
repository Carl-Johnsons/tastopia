using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using UserService.Application.Configs.MapperConverters;
using UserService.Domain.Entities;
using UserService.Domain.Responses;

namespace UserService.Application.Configs;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GetUserDetailsResponse>().ReverseMap();
        CreateMap<User, AdminGetUserDetailResponse>().ReverseMap();
        CreateMap<UserDetailsDTO, GetUserDetailsResponse>().ReverseMap();
        CreateMap<UserReport, AdminUserReportResponse>().ReverseMap();

        // Grpc mapping
        CreateMap(typeof(List<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldConverter<,>));
        CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListConverter<,>));
    }
}

