using System.Net;

namespace UploadFileService.Domain.Errors;

public class CloudinaryFileError
{
    public static Error NotFound =>
        new("CloudinaryFileError.NotFound",
           StatusCode: (int)HttpStatusCode.NotFound,
           Message: "Cloudinary file not found");
    public static Error AlreadyExistCloudinaryFile =>
        new("CloudinaryFileError.AlreadyExistCloudinaryFile",
            StatusCode: (int)HttpStatusCode.Conflict,
            Message: "They already have the cloudinary file, abort adding addition cloudinary file");
    public static Error UploadToCloudFail =>
        new("CloudinaryFileError.UploadToCloudFail",
            StatusCode: (int)HttpStatusCode.InternalServerError,
            Message: "Upload file to cloudinary fail");
    public static Error DeleteToCloudFail =>
        new("CloudinaryFileError.DeleteToCloudFail",
            StatusCode: (int)HttpStatusCode.InternalServerError,
            Message: "Delete file to cloudinary fail");
    public static Error FileListContainNull =>
            new("CloudinaryFileError.FileListContainNull",
                StatusCode: (int)HttpStatusCode.BadRequest,
                Message: "File list contains a null element.");
    public static Error FileListTooLarge(long MaxSize, long FileSize) =>
        new("CloudinaryFileError.FileListTooLarge",
                StatusCode: (int)HttpStatusCode.BadRequest,
                Message: $"File list contains a large element, max size: {MaxSize}, chosen file size: {FileSize}");

    public static Error InvalidFile(string Type, string ChosenFileType) =>
        new("CloudinaryFileError.InvalidFile",
            StatusCode: (int)HttpStatusCode.BadRequest,
            Message: $"Invalid file {Type}, chosen file:{ChosenFileType}");
}
