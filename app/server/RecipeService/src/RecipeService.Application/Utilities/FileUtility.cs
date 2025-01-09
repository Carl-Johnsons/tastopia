using Contract.DTOs.UploadFileDTO;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using UploadFileProto;

namespace RecipeService.Application.Utilities;

public static class FileUtility
{
    public static async Task<List<GrpcFileStreamDTO>> ConvertIFormFileToGrpcFileStreamDTOAsync(List<IFormFile> files)
    {
        var tasks = files.Select((file, index) => Task.Run(async () => new
        {
            Index = index,
            GrpcFileStreamDTO = new GrpcFileStreamDTO
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                Stream = await ByteString.FromStreamAsync(file.OpenReadStream())
            }
        }));
        var results = await Task.WhenAll(tasks);
        return results.OrderBy(result => result.Index).Select(result => result.GrpcFileStreamDTO).ToList();
    }

    public static async Task<List<FileStreamDTO>> ConvertIFormFileToFileStreamDTOAsync(List<IFormFile> files)
    {
        var tasks = files.Select((file, index) => Task.Run(() => new
        {
            Index = index,
            FileStreamDTO = new FileStreamDTO
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                Stream = new BinaryReader(file.OpenReadStream()).ReadBytes((int)file.Length)
            }
        }));

        var results = await Task.WhenAll(tasks);
        return results.OrderBy(result => result.Index).Select(result => result.FileStreamDTO).ToList();
    }
}