using AutoMapper;
using DuendeIdentityServer.Models;

namespace DuendeIdentityServer.DTOs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ApplicationUser, ApplicationUserResponseDTO>().ReverseMap();
            config.CreateMap<ApplicationUser, UpdateUserDTO>().ReverseMap();

        });

        
        return mappingConfig;
    }
}