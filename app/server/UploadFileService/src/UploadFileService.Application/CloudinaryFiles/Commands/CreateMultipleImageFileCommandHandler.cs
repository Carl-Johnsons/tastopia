using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using UploadFileService.Application.Utilities;
using UploadFileService.Domain.Responses;

namespace UploadFileService.Application.CloudinaryFiles.Commands;
public record CreateMultipleImageFileCommand : IRequest<Result<List<FileResponse>?>>
{
    [Required]
    public List<IFormFile>? FormFiles { get; init; } = null!;
}
public class CreateMultipleImageFileCommandHandler : IRequestHandler<CreateMultipleImageFileCommand, Result<List<FileResponse>?>>
{
    private readonly IFileUtility _fileUtility;
    private readonly Cloudinary _cloudinary;
    private readonly ApplicationFileUtility _applicationFileUtility;

    public CreateMultipleImageFileCommandHandler(Cloudinary cloudinary, IFileUtility fileUtility, ApplicationFileUtility applicationFileUtility)
    {
        _cloudinary = cloudinary;
        _fileUtility = fileUtility;
        _applicationFileUtility = applicationFileUtility;
    }

    public async Task<Result<List<FileResponse>?>> Handle(CreateMultipleImageFileCommand request, CancellationToken cancellationToken)
    {
        var formFiles = request.FormFiles;

        if (formFiles == null || !formFiles.Any())
        {
            return Result<List<FileResponse>?>.Failure(CloudinaryFileError.NotFound);
        }

        foreach(var file in formFiles)
        {
            if(file == null)
            {
                return Result<List<FileResponse>?>.Failure(CloudinaryFileError.FileListContainNull);
            }
            if (file.Length > 10 * 1024 * 1024)
            {
                return Result<List<FileResponse>?>.Failure(CloudinaryFileError.FileListTooLarge(10*1024*1024, file.Length));
            }
        }

        var returnResult = Enumerable.Repeat(new FileResponse(), formFiles.Count).ToList();
        var uploadResults = Enumerable.Repeat(new ImageUploadResult(), formFiles.Count).ToList();

        var formFileNames = Enumerable.Repeat(string.Empty, formFiles.Count).ToList();
        var formFileSizes = Enumerable.Repeat(0L, formFiles.Count).ToList();
        var extensionValues = Enumerable.Repeat(string.Empty, formFiles.Count).ToList();

        Result? fileError = null;

        try {

            var tasks = formFiles.Select(async (formFile, index) =>
            {
                string fileName = formFile.FileName;
                Stream fileStream = formFile.OpenReadStream();
                formFileNames[index] = fileName;
                formFileSizes[index] = fileStream.Length;
                extensionValues[index] = Path.GetExtension(fileName);
                if (_fileUtility.GetFileType(fileName) != IFileUtility.FileType.IMAGE)
                {
                    fileError = CloudinaryFileError.InvalidFile("Image", Path.GetExtension(fileName));
                    return;
                }
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, fileStream),
                    PublicId = Guid.NewGuid().ToString(),
                    Folder = Environment.GetEnvironmentVariable("Cloudinary_Folder"),
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                uploadResults[index] = uploadResult;
            }).ToList();

            await Task.WhenAll(tasks);

            if (fileError != null)
            {
                await _applicationFileUtility.RollBackImageFile(uploadResults.Select(u => u.PublicId).ToList());
                return Result<List<FileResponse>?>.Failure(fileError.Errors);
            }
            //check if all image upload to cloud susscess
            if (uploadResults.Any(result => result == null || result.StatusCode != HttpStatusCode.OK))
            {
                await _applicationFileUtility.RollBackImageFile(uploadResults.Select(u => u.PublicId).ToList());
                return Result<List<FileResponse>?>.Failure(CloudinaryFileError.UploadToCloudFail);
            }
            for (int i = 0; i < uploadResults.Count; i++)
            {
                returnResult[i] = new FileResponse
                {
                    Name = formFileNames[i],
                    PublicId = uploadResults[i].PublicId,
                    Url = uploadResults[i].Url.ToString(),
                    Size = formFileSizes[i],
                    Extension = extensionValues[i]
                };
            }

            if (returnResult == null || returnResult.Count != formFiles.Count)
            {
                await _applicationFileUtility.RollBackImageFile(uploadResults.Select(u => u.PublicId).ToList());
                return Result<List<FileResponse>?>.Failure(CloudinaryFileError.UploadToCloudFail);
            }

            return Result<List<FileResponse>?>.Success(returnResult);

        } catch(Exception ex) {

            await _applicationFileUtility.RollBackImageFile(uploadResults.Select(u => u.PublicId).ToList());
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<List<FileResponse>?>.Failure(CloudinaryFileError.UploadToCloudFail);
        }
    }
    //private async void RollBackCloudinary(List<ImageUploadResult> uploadResults)
    //{
    //    var tasks = uploadResults.Select(async (result, index) =>
    //    {
    //        if (result == null || result.PublicId == null) return;
    //        var deleteParams = new DeletionParams(result.PublicId)
    //        {
    //            ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType),result.ResourceType),
    //        };
    //        var deleteResult = await _cloudinary.DestroyAsync(deleteParams);
    //    }).ToList();
    //    await Task.WhenAll(tasks);
    //}
}
