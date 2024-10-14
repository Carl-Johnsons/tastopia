using Contract.Event.UploadEvent.EventModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

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
    public string getFileType(string fileName)
    {


        if (fileName == null || fileName.Length == 0)
        {
            return "Invalid";
        }

        string extension = Path.GetExtension(fileName);
        if (imageExtensions.Contains(extension))
        {
            return "Image";
        }

        else if (videoExtensions.Contains(extension))
        {
            return "Video";
        }

        else
        {
            return "Raw";
        }
    }

    public List<IFormFile> convertFileStreamsToIFormFile(List<FileStreamEvent> fileStreamEvents)
    {
        List<IFormFile> formFiles = new List<IFormFile>(fileStreamEvents.Count);
        for (int i = 0; i < fileStreamEvents.Count; i++)
        {
            var fileStreamEvent = fileStreamEvents[i];
            var fileStream = new MemoryStream(fileStreamEvent.Stream);
            IFormFile formFile = new FormFile(fileStream, 0, fileStreamEvent.Stream.Length, fileStreamEvent.FileName, fileStreamEvent.FileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = fileStreamEvent.ContentType,
            };
            formFiles.Add(formFile);
        }
        return formFiles;
    }
}
