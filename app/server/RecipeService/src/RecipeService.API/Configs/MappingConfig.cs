using AutoMapper;
using Contract.DTOs.RecipeDTO;
using RecipeProto;
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
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
                .ForMember(dest => dest.CookTime, opt => opt.MapFrom(src => src.CookTime))
                .ReverseMap()
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
                .ForMember(dest => dest.CookTime, opt =>
                    opt.MapFrom(src => !string.IsNullOrEmpty(src.CookTime) ? src.CookTime : ""));
            
            config.CreateMap<UpdateRecipeDTO, UpdateRecipeCommand>()
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps)).ReverseMap();

            config.CreateMap<DTOs.UpdateStepDTO, Application.Recipes.Commands.UpdateStepDTO>().ReverseMap();

            config.CreateMap<RecipeDetailsDTO, Recipe>()
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps)).ReverseMap();

            //Grpc mapping
            config.CreateMap<GrpcRecipeDetailsDTO, Recipe>()
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
                .ForMember(dest => dest.CookTime, opt => opt.MapFrom(src => src.CookTime))
                .ReverseMap()
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps))
                .ForMember(dest => dest.CookTime, opt =>
                    opt.MapFrom(src => !string.IsNullOrEmpty(src.CookTime) ? src.CookTime : ""));

            config.CreateMap<GrpcStepDTO, Step>().ReverseMap();

            config.CreateMap<GrpcTagDTO, Tag>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => Enum.Parse<TagStatus>(src.Status))
                 )
                 .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => Enum.Parse<TagCategory>(src.Category))
                 )
                .ReverseMap()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString())
                )
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category.ToString())
                );

            /////////////////////////////////////////////////////


            config.CreateMap<Contract.DTOs.RecipeDTO.StepDTO, Step>().ReverseMap();

            config.CreateMap<TagDTO, Tag>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => Enum.Parse<TagStatus>(src.Status))
                 )
                 .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => Enum.Parse<TagCategory>(src.Category))
                 )
                .ReverseMap()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString())
                )
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category.ToString())
                );

            
        });

        return mappingConfig;
    }
}
