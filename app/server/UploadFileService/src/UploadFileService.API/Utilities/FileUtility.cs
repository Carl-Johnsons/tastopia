using Contract.DTOs.UploadFileDTO;
using UploadFileProto;

namespace UploadFileService.API.Utilities;

public static class FileUtility
{
    public static async Task<List<IFormFile>> ConvertGrpcFileStreamToIFormFileAsync(List<GrpcFileStreamDTO> streams)
    {
        var tasks = streams.Select((grpcFileStream, index) => Task.Run(async () => new
        {
            Index = index,
            FormFile = (IFormFile)new FormFile(new MemoryStream(await Task.Run(() => grpcFileStream.Stream.ToByteArray())), 0, grpcFileStream.Stream.Length, grpcFileStream.FileName, grpcFileStream.FileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = grpcFileStream.ContentType,
            }
        }));

        var results = await Task.WhenAll(tasks);
        return results.OrderBy(result => result.Index).Select(result => result.FormFile).ToList();
    }

    public static async Task<List<IFormFile>> ConvertFileStreamDTOToIFormFileAsync(List<FileStreamDTO> streams)
    {
        var tasks = streams.Select((grpcFileStream, index) => Task.Run(async () => new
        {
            Index = index,
            FormFile = (IFormFile)new FormFile(new MemoryStream(await Task.Run(() => grpcFileStream.Stream)), 0, grpcFileStream.Stream.Length, grpcFileStream.FileName, grpcFileStream.FileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = grpcFileStream.ContentType,
            }
        }));

        var results = await Task.WhenAll(tasks);
        return results.OrderBy(result => result.Index).Select(result => result.FormFile).ToList();
    }
}