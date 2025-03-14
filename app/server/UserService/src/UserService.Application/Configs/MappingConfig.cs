using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using UserService.Application.Configs.MapperConverters;
using UserService.Domain.Entities;
using UserService.Domain.Responses;

namespace UserService.Application.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<User, GetUserDetailsResponse>().ReverseMap();
            config.CreateMap<User, AdminGetUserDetailResponse>().ReverseMap();
            config.CreateMap<UserDetailsDTO, GetUserDetailsResponse>().ReverseMap();

            // Grpc mapping
            config.CreateMap(typeof(List<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldConverter<,>));
            config.CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListConverter<,>));
        });


        return mappingConfig;
    }
}
