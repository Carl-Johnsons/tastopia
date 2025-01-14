using AutoMapper;
using Google.Protobuf.Collections;

namespace TrackingService.Application.Configs.MapperConverters;

internal class ListToRepeatedFieldConverter<TSource, TDestination> : ITypeConverter<List<TSource>, RepeatedField<TDestination>>
{
    public RepeatedField<TDestination> Convert(List<TSource> source, RepeatedField<TDestination> destination, ResolutionContext context)
    {
        destination = destination ?? new RepeatedField<TDestination>();
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