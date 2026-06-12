using AutoMapper;
using Contract.DTOs.UploadFileDTO;
using Google.Protobuf;
using Google.Protobuf.Collections;
using UploadFileProto;
using UploadFileService.API.Configs.MapperConverters;
using UploadFileService.Domain.Responses;

namespace UploadFileService.API.Configs;

public class UploadProfile : Profile
{
    public UploadProfile()
    {
        CreateMap<FileDTO, FileResponse>().ReverseMap();
        // Grpc mapping
        CreateMap(typeof(List<>), typeof(RepeatedField<>)).ConvertUsing(typeof(ListToRepeatedFieldConverter<,>));
        CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(RepeatedFieldToListConverter<,>));

        CreateMap<GrpcFileStreamDTO, FileStreamDTO>()
            .ForMember(dest => dest.Stream, otp => otp
                .MapFrom(src => src.Stream.ToByteArray()))
            .ReverseMap()
                .ForMember(dest => dest.Stream, otp => otp
                    .MapFrom(src => ByteString.CopyFrom(src.Stream)));

        CreateMap<GrpcFileDTO, FileResponse>().ReverseMap();
    }
}
