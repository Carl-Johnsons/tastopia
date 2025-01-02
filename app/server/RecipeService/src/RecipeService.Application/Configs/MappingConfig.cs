using AutoMapper;

namespace RecipeService.Application.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            //config.CreateMap<User, GetUserDetailsResponse>().ReverseMap();
        });


        return mappingConfig;
    }
}
