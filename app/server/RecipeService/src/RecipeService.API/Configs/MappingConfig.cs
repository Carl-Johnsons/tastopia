using AutoMapper;
using RecipeService.API.DTOs;
using RecipeService.Application.Recipes;

namespace RecipeService.API.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {

            config.CreateMap<CreateRecipeDTO, CreateRecipeCommand>()
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps)).ReverseMap();

            config.CreateMap<DTOs.StepDTO, Application.Recipes.StepDTO>().ReverseMap();
        });

        return mappingConfig;
    }
}
