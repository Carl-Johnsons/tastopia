using AutoMapper;

namespace TrackingService.Application.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {

        });



        return mappingConfig;
    }
}
