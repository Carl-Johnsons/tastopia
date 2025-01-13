using AutoMapper;
using Google.Protobuf.Collections;

namespace TrackingService.Application.Configs.MapperConverters;

internal class RepeatedFieldToListConverter<TSource, TDestination> : ITypeConverter<RepeatedField<TSource>, List<TDestination>>
{
    public List<TDestination> Convert(RepeatedField<TSource> source, List<TDestination> destination, ResolutionContext context)
    {
        destination ??= new List<TDestination>();
        foreach (var item in source)
        {
            // obviously we haven't performed the mapping for the item yet
            // since AutoMapper didn't recognise the list conversion
            // so we need to map the item here and then add it to the new
            // collection
            destination.Add(context.Mapper.Map<TDestination>(item));
        }
        return destination;
    }
}