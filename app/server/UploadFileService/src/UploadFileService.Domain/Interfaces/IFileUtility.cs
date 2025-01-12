using Contract.DTOs.UploadFileDTO;
using Microsoft.AspNetCore.Http;
using UploadFileProto;

namespace UploadFileService.Domain.Interfaces;

public interface IFileUtility
{
    public enum FileType
    {
        INVALID = 0,
        IMAGE = 1,
        VIDEO = 2,
        RAW = 3
    }
    FileType GetFileType(string fileName);
    string? GetPublicIdByUrl(string url);

    Task<List<IFormFile>> ConvertGrpcFileStreamToIFormFileAsync(List<GrpcFileStreamDTO> streams);
    Task<List<IFormFile>> ConvertFileStreamDTOToIFormFileAsync(List<FileStreamDTO> streams);

}