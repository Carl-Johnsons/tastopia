using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using RecipeProto;
using RecipeService.Application.Configs.MapperConverters;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.Configs;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Comment, RecipeCommentResponse>()
           .ForMember(dest => dest.DisplayName, opt => opt.Ignore())
           .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
           .ReverseMap();

        CreateMap<SimpleRecipeResponse, Recipe>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.RecipeImgUrl))
        .ReverseMap();

        CreateMap<Tag, AdminTagResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse(typeof(TagStatus), src.Status)));

        // Grpc mapping
        CreateMap(typeof(List<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldConverter<,>));
        CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListConverter<,>));

        CreateMap<GetSimpleUsersDTO, GrpcGetSimpleUsersDTO>()
            .ForMember(dest => dest.Users,
                opt => opt.MapFrom(src => src.Users.ToDictionary(
                    user => user.Key,
                    user => new CommonProto.GrpcSimpleUser
                    {
                        AccountId = user.Value.AccountId.ToString(),
                        AvtUrl = user.Value.AvtUrl,
                        DisplayName = user.Value.DisplayName
                    }))).ReverseMap();

        CreateMap<SimpleRecipeResponse, GrpcSimpleRecipe>()
            .ReverseMap();
    }
}