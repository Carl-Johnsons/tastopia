using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using NotificationService.Application.Configs.MapperConverters;
using UserProto;

namespace NotificationService.Application.Configs;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
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
    }
}