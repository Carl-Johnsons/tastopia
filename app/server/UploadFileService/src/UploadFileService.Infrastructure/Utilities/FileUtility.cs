using Contract.DTOs.UploadFileDTO;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using UploadFileProto;

namespace UploadFileService.Infrastructure.Utilities;

public class FileUtility : IFileUtility
{
    private readonly List<string> imageExtensions = new()
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".tif", ".webp",
            ".svg", ".ico", ".heic", ".heif", ".raw", ".cr2", ".nef", ".orf",
            ".sr2", ".arw", ".dng", ".raf", ".3fr", ".kdc", ".mos", ".mef",
            ".nrw", ".rw2", ".pef"
        };
    private readonly List<string> videoExtensions = new()
        {
            ".mp4", ".avi", ".mkv", ".mov", ".wmv", ".flv", ".webm", ".mpeg",
            ".mpg", ".m4v", ".3gp", ".3g2", ".vob", ".ogv", ".m2ts", ".mts",
            ".ts", ".mxf", ".f4v", ".rm", ".rmvb", ".divx", ".xvid", ".dv",
            ".asf", ".swf", ".m2v",".mp3", ".wav", ".aac", ".flac", ".ogg", ".wma", ".m4a",
            ".aiff", ".alac", ".opus", ".amr"
        };

    private readonly string foler = Environment.GetEnvironmentVariable("Cloudinary_Folder") ?? "file_storage";

    public IFileUtility.FileType GetFileType(string fileName)
    {


        if (fileName == null || fileName.Length == 0)
        {
            return IFileUtility.FileType.INVALID;
        }

        string extension = Path.GetExtension(fileName);
        if (imageExtensions.Contains(extension))
        {
            return IFileUtility.FileType.IMAGE;
        }

        else if (videoExtensions.Contains(extension))
        {
            return IFileUtility.FileType.VIDEO;
        }

        else
        {
            return IFileUtility.FileType.RAW;
        }
    }



    public string? GetPublicIdByUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return null;
        }

        if(!url.Contains(foler))
        {
            return null;
        }

        string pattern = @"upload\/(?:v\d+\/)?(.+?)\.(\w+)$";

        Match match = Regex.Match(url, pattern);

        if (match.Success)
        {
            string publicId = match.Groups[1].Value;
            return publicId;
        }

        return null;
    }

    public async Task<List<IFormFile>> ConvertGrpcFileStreamToIFormFileAsync(List<GrpcFileStreamDTO> streams)
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

    public async Task<List<IFormFile>> ConvertFileStreamDTOToIFormFileAsync(List<FileStreamDTO> streams)
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
