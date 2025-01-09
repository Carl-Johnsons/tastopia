using AutoMapper;
using Google.Protobuf.Collections;
using Grpc.Core;
using Newtonsoft.Json;
using UploadFileProto;
using UploadFileService.API.Utilities;
using UploadFileService.Application.CloudinaryFiles.Commands;

namespace UploadFileService.API.GrpcServices;

public class GrpcUploadFileService : GrpcUploadFile.GrpcUploadFileBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GrpcUploadFileService(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override Task<GrpcEmpty> DeleteMultipleImage(GrpcDeleteMultipleImageRequest request, ServerCallContext context)
    {
        return base.DeleteMultipleImage(request, context);
    }

    public override Task<GrpcListFileDTO> UpdateMultipleImage(GrpcUpdateMultipleImageRequest request, ServerCallContext context)
    {
        return base.UpdateMultipleImage(request, context);
    }

    public override async Task<GrpcListFileDTO> UploadMultipleImage(GrpcUploadMultipleImageRequest request, ServerCallContext context)
    {
        var formFiles = await FileUtility.ConvertGrpcFileStreamToIFormFileAsync(request.FileStreams.ToList());
        var response = await _sender.Send(new CreateMultipleCloudinaryImageFileCommand
        {
            FormFiles = formFiles
        });

        response.ThrowIfFailure();
        var result = new GrpcListFileDTO();
        var list = new List<GrpcFileDTO>();
        foreach (var cloudinaryFile in response.Value!)
        {
            result.Files.Add(new GrpcFileDTO
            {
                Name = cloudinaryFile.Name,
                PublicId = cloudinaryFile.PublicId,
                Size = cloudinaryFile.Size,
                Url = cloudinaryFile.Url
            });
        }
        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
        return result;
    }
}
