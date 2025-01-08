using AutoMapper;

namespace TrackingService.API.Configs;

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
