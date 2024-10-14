namespace UploadFileService.Domain.Errors;

public class CloudinaryFileError
{
    public static Error NotFound =>
        new("CloudinaryFileError.NotFound",
            "Cloudinary file not found");
    public static Error AlreadyExistCloudinaryFile =>
        new("CloudinaryFileError.AlreadyExistCloudinaryFile",
            "They already have the cloudinary file, abort adding addition cloudinary file");
    public static Error UploadToCloudFail =>
        new("CloudinaryFileError.UploadToCloudFail", "Upload file to cloudinary fail");
    public static Error DeleteToCloudFail =>
        new("CloudinaryFileError.DeleteToCloudFail", "Delete file to cloudinary fail");
    public static Error FailOperation =>
            new("CloudinaryFileError.FailOperation", "Fail operation to upload cloudinary file");
    public static Error FileListContainNull =>
            new("CloudinaryFileError.FileListContainNull", "File list contains a null element.");

    public static Error InvalidFile(string Type, string ChosenFileType) =>
        new("CloudinaryFileError.InvalidFile", $"Invalid file {Type}, chosen file:{ChosenFileType}");
}
