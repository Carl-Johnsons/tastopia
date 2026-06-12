using AutoMapper;
using Google.Protobuf.Collections;
using TrackingService.Application.Configs.MapperConverters;

namespace TrackingService.Application.Configs;

public class TrackingProfile : Profile
{
    public TrackingProfile()
    {
        //Grpc
        CreateMap(typeof(List<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldConverter<,>));
        CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListConverter<,>));
    }
}