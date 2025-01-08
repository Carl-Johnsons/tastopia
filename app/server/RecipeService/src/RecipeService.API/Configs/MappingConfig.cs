using AutoMapper;
using Contract.DTOs.RecipeDTO;
using RecipeService.API.DTOs;
using RecipeService.Application.Recipes.Commands;
using RecipeService.Domain.Entities;

namespace RecipeService.API.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {

            config.CreateMap<CreateRecipeDTO, CreateRecipeCommand>()
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps)).ReverseMap();

            config.CreateMap<DTOs.StepDTO, Application.Recipes.Commands.StepDTO>().ReverseMap();

            config.CreateMap<RecipeDetailsDTO, Recipe>()
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps)).ReverseMap();

            config.CreateMap<Contract.DTOs.RecipeDTO.StepDTO, Step>().ReverseMap();

            config.CreateMap<TagDTO, Tag>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => Enum.Parse<TagStatus>(src.Status))
                 )
                .ReverseMap()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString())
                );
        });

        return mappingConfig;
    }
}
