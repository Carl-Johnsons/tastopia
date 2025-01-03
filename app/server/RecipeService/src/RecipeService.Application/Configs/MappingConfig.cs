using AutoMapper;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;

namespace RecipeService.Application.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Comment, RecipeCommentResponse>().ReverseMap();
        });


        return mappingConfig;
    }
}
