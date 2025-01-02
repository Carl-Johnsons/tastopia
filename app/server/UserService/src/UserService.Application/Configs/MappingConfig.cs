using AutoMapper;
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
        });


        return mappingConfig;
    }
}
