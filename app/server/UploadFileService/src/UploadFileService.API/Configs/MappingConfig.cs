using AutoMapper;
using Contract.DTOs.UploadFileDTO;
using Google.Protobuf;
using Google.Protobuf.Collections;
using UploadFileProto;
using UploadFileService.API.Configs.MapperConverters;
using UploadFileService.Domain.Entities;

namespace UploadFileService.API.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            
            // Grpc mapping
            config.CreateMap(typeof(List<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldConverter<,>));
            config.CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListConverter<,>));

            config.CreateMap<GrpcFileStreamDTO, FileStreamDTO>()
                .ForMember(dest => dest.Stream, otp => otp
                    .MapFrom(src => src.Stream.ToByteArray()))
            .ReverseMap()
                .ForMember(dest => dest.Stream, otp => otp
                    .MapFrom(src => ByteString.CopyFrom(src.Stream)));

            config.CreateMap<GrpcFileDTO, CloudinaryFile>().ReverseMap();
        });
        //mappingConfig.AssertConfigurationIsValid();

        return mappingConfig;
    }
}
