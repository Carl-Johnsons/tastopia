using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using NotificationService.Application.Configs.MapperConverters;
using UserProto;

namespace NotificationService.Application.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            // Grpc mapping
            config.CreateMap(typeof(List<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldConverter<,>));
            config.CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListConverter<,>));

            config.CreateMap<GetSimpleUsersDTO, GrpcGetSimpleUsersDTO>()
                .ForMember(dest => dest.Users,
                    opt => opt.MapFrom(src => src.Users.ToDictionary(
                        user => user.Key,
                        user => new CommonProto.GrpcSimpleUser
                        {
                            AccountId = user.Value.AccountId.ToString(),
                            AvtUrl = user.Value.AvtUrl,
                            DisplayName = user.Value.DisplayName
                        }))).ReverseMap();
        });
        //mappingConfig.AssertConfigurationIsValid();

        return mappingConfig;
    }
}
