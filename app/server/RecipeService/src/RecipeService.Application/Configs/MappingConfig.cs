using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using MassTransit.DependencyInjection.Registration;
using RecipeProto;
using RecipeService.Application.Configs.MapperConverters;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<Comment, RecipeCommentResponse>()
                .ForMember(dest => dest.DisplayName, opt => opt.Ignore())
                .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
                .ReverseMap();

            config.CreateMap<Recipe, SimpleRecipeResponse>()
                .ForMember(dest => dest.AuthorDisplayName, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorAvtUrl, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeImgUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ReverseMap()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.RecipeImgUrl));

            // Grpc mapping
            config.CreateMap(typeof(List<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldConverter<,>));
            config.CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListConverter<,>));

            config.CreateMap<GetSimpleUsersDTO, GrpcGetSimpleUsersDTO>()
                .ForMember(dest => dest.Users,
                    opt => opt.MapFrom(src => src.Users.ToDictionary(
                        user => user.Key,
                        user => new GrpcSimpleUser
                        {
                            AccountId = user.Value.AccountId.ToString(),
                            AvtUrl = user.Value.AvtUrl,
                            DisplayName = user.Value.DisplayName
                        }))).ReverseMap();

            config.CreateMap<GrpcSimpleRecipe, SimpleRecipeResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.Vote, opt => opt.MapFrom(src => Enum.Parse<Vote>(src.Vote)))
            .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Vote, opt => opt.MapFrom(src => src.ToString()));

        });
        //mappingConfig.AssertConfigurationIsValid();

        return mappingConfig;
    }
}
