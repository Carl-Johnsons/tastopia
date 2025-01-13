using AutoMapper;
using Google.Protobuf.Collections;
using TrackingService.Application.Configs.MapperConverters;
using TrackingService.Domain.Responses;

namespace TrackingService.Application.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            //Grpc
            config.CreateMap(typeof(List<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldConverter<,>));
            config.CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListConverter<,>));
        });



        return mappingConfig;
    }
}
