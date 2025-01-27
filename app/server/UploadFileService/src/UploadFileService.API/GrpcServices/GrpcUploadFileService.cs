using AutoMapper;
using Google.Protobuf.Collections;
using Grpc.Core;
using Newtonsoft.Json;
using UploadFileProto;
using UploadFileService.Application.Files.Commands;
using UploadFileService.Domain.Responses;

namespace UploadFileService.API.GrpcServices;

public class GrpcUploadFileService : GrpcUploadFile.GrpcUploadFileBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IFileUtility _fileUtility;

    public GrpcUploadFileService(ISender sender, IMapper mapper, IFileUtility fileUtility)
    {
        _sender = sender;
        _mapper = mapper;
        _fileUtility = fileUtility;
    }

    public override Task<GrpcEmpty> DeleteMultipleImage(GrpcDeleteMultipleImageRequest request, ServerCallContext context)
    {
        return base.DeleteMultipleImage(request, context);
    }

    public override async Task<GrpcListFileDTO> UpdateMultipleImage(GrpcUpdateMultipleImageRequest request, ServerCallContext context)
    {
        var formFiles = await _fileUtility.ConvertGrpcFileStreamToIFormFileAsync(request.FileStreams.ToList());
        var deleteUrls = request.DeleteUrls.ToList();

        var listFileResponse = new List<FileResponse>();

        if(formFiles != null && formFiles.Count != 0)
        {
            var response = await _sender.Send(new UpdateMultipleImageFileCommand
            {
                FormFiles = formFiles,
                DeleteUrls = deleteUrls,
            });
            response.ThrowIfFailure();
            listFileResponse = response.Value;
        }

        var files = new RepeatedField<GrpcFileDTO>();
        if (listFileResponse != null && listFileResponse.Count != 0)
        {
            foreach (var file in listFileResponse)
            {
                files.Add(new GrpcFileDTO
                {
                    Size = file.Size,
                    Name = file.Name,
                    PublicId = file.PublicId,
                    Url = file.Url,
                });
            }
        }
        var result = new GrpcListFileDTO
        {
            Files = {files}
        };
        return result;
    }

    public override async Task<GrpcListFileDTO> UploadMultipleImage(GrpcUploadMultipleImageRequest request, ServerCallContext context)
    {
        var formFiles = await _fileUtility.ConvertGrpcFileStreamToIFormFileAsync(request.FileStreams.ToList());
        var response = await _sender.Send(new CreateMultipleImageFileCommand
        {
            FormFiles = formFiles
        });

        response.ThrowIfFailure();
        var result = new GrpcListFileDTO();
        foreach (var file in response.Value!)
        {
            result.Files.Add(new GrpcFileDTO
            {
                Name = file.Name,
                PublicId = file.PublicId,
                Size = file.Size,
                Url = file.Url
            });
        }
        return result;
    }
}
