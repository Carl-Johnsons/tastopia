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
    string? GetPublicIdByUrl(string fileName);
}