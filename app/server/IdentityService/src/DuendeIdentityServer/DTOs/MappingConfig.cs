using AutoMapper;
using Contract.DTOs.UserDTO;
using IdentityService.Application.Account.Commands;

namespace DuendeIdentityServer.DTOs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<RegisterAccountDTO, RegisterAccountCommand>().ReverseMap();
            config.CreateMap<ApplicationAccount, ApplicationUserResponseDTO>().ReverseMap();
            config.CreateMap<VerifyAccountDTO, VerifyAccountCommand>().ReverseMap();
            config.CreateMap<ApplicationAccount, AccountDTO>().ReverseMap();
        });


        return mappingConfig;
    }
}