using AutoMapper;
using Google.Protobuf.Collections;
using IdentityService.Application.Configs.MapperConverters;

namespace IdentityService.Application.Configs;

public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap(typeof(List<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldConverter<,>));
        CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListConverter<,>));
    }
}
