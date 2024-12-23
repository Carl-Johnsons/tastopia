using AutoMapper;
using IdentityService.Application.Account;

namespace DuendeIdentityServer.DTOs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<RegisterAccountDTO, RegisterAccountCommand>().ReverseMap();
            config.CreateMap<ApplicationAccount, ApplicationUserResponseDTO>().ReverseMap();
            config.CreateMap<ApplicationAccount, UpdateUserDTO>().ReverseMap();
        });


        return mappingConfig;
    }
}