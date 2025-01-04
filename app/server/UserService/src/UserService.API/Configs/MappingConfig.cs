using AutoMapper;
using Contract.DTOs.UserDTO;
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
        });


        return mappingConfig;
    }
}
