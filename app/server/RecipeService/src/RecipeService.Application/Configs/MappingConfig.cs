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
            config.CreateMap<Comment, RecipeCommentResponse>()
                .ForMember(dest => dest.DisplayName, opt => opt.Ignore())  // Bỏ qua ánh xạ cho DisplayName
                .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())   // Bỏ qua ánh xạ cho AvatarUrl
                .ReverseMap();
        });



        return mappingConfig;
    }
}
