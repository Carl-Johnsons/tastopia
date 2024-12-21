using AutoMapper;

namespace DuendeIdentityServer.DTOs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ApplicationAccount, ApplicationUserResponseDTO>().ReverseMap();
            config.CreateMap<ApplicationAccount, UpdateUserDTO>().ReverseMap();

        });

        
        return mappingConfig;
    }
}