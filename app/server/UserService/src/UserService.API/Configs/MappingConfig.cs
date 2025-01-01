using AutoMapper;

namespace UserService.API.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            //config.CreateMap<Us, AccountDTO>().ReverseMap();
        });


        return mappingConfig;
    }
}
