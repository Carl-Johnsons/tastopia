using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using UploadFileService.Application.Utilities;
using UploadFileService.Domain.Responses;

namespace UploadFileService.Application.Files.Commands;
public record UpdateMultipleImageFileCommand : IRequest<Result<List<FileResponse>?>>
{
    public List<string>? DeleteUrls { get; init; } = null!;
    public List<IFormFile>? FormFiles { get; init; } = null!;
}
public class UpdateMultipleImageFileCommandHandler : IRequestHandler<UpdateMultipleImageFileCommand, Result<List<FileResponse>?>>
{
    private readonly IFileUtility _fileUtility;
    private readonly Cloudinary _cloudinary;
    private readonly ApplicationFileUtility _applicationFileUtility;

    public UpdateMultipleImageFileCommandHandler(Cloudinary cloudinary, IFileUtility fileUtility, ApplicationFileUtility applicationFileUtility)
    {
        _cloudinary = cloudinary;
        _fileUtility = fileUtility;
        _applicationFileUtility = applicationFileUtility;
    }

    public async Task<Result<List<FileResponse>?>> Handle(UpdateMultipleImageFileCommand request, CancellationToken cancellationToken)
    {
        var formFiles = request.FormFiles;
        var urls = request.DeleteUrls;

        var result = new List<FileResponse>();

        if (formFiles != null && formFiles.Any())
        {
            var upload = await UploadMultipleImage(formFiles, cancellationToken);
            result = upload.Value;
            if (upload.IsFailure)
            {
                return upload;
            }
        }
        if (urls != null && urls.Any())
        {
            await DeleteMultipleImage(urls);
        }

        if ((formFiles == null || formFiles.Count == 0) && (urls == null || urls.Count == 0))
        {
            return Result<List<FileResponse>?>.Failure(CloudinaryFileError.NotFound);
        }
        return Result<List<FileResponse>?>.Success(result);
    }

    private async Task<Result<List<FileResponse>?>> UploadMultipleImage(List<IFormFile> formFiles, CancellationToken cancellationToken)
    {
        foreach (var file in formFiles)
        {
            if (file == null)
            {
                return Result<List<FileResponse>?>.Failure(CloudinaryFileError.FileListContainNull);
            }
            if (file.Length > 10 * 1024 * 1024)
            {
                return Result<List<FileResponse>?>.Failure(CloudinaryFileError.FileListTooLarge(10 * 1024 * 1024, file.Length));
            }
        }

        var returnResult = Enumerable.Repeat(new FileResponse(), formFiles.Count).ToList();
        var uploadResults = Enumerable.Repeat(new ImageUploadResult(), formFiles.Count).ToList();
        var formFileNames = Enumerable.Repeat(string.Empty, formFiles.Count).ToList();
        var formFileSizes = Enumerable.Repeat(0L, formFiles.Count).ToList();
        var extensionValues = Enumerable.Repeat(string.Empty, formFiles.Count).ToList();

        try
        {
            Result? fileError = null;
            var tasks = formFiles.Select(async (formFile, index) =>
            {
                string fileName = formFile.FileName;
                Stream fileStream = formFile.OpenReadStream();
                formFileNames[index] = fileName;
                formFileSizes[index] = fileStream.Length;
                extensionValues[index] = Path.GetExtension(fileName);
                if (_fileUtility.GetFileType(fileName) != IFileUtility.FileType.IMAGE)
                {
                    fileError = Result<List<FileResponse>?>.Failure(CloudinaryFileError.InvalidFile("Image", Path.GetExtension(fileName)));
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
        }
        catch (Exception ex)
        {
            return Result<List<FileResponse>?>.Failure(CloudinaryFileError.UploadToCloudFail);
        }
    }

    private async Task DeleteMultipleImage(List<string> urls)
    {
        try {
            var setPublicId = new HashSet<string?>();
            foreach (var url in urls)
            {
                setPublicId.Add(_fileUtility.GetPublicIdByUrl(url));
            }
            var tasks = setPublicId.Select(async (publicId, index) =>
            {
                if (string.IsNullOrEmpty(publicId))
                {
                    return;
                }
                var deleteParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Image,
                };
                await _cloudinary.DestroyAsync(deleteParams);
            }).ToList();
            await Task.WhenAll(tasks);
            return;
        } catch (Exception ex)
        {
            return;
        }
    }


    //private async void RollBackCloudinary(List<ImageUploadResult> uploadResults)
    //{
    //    var tasks = uploadResults.Select(async (result, index) =>
    //    {
    //        if (result == null || result.PublicId == null) return;
    //        var deleteParams = new DeletionParams(result.PublicId)
    //        {
    //            ResourceType = ResourceType.Image,
    //        };
    //        await _cloudinary.DestroyAsync(deleteParams);
    //    }).ToList();
    //    await Task.WhenAll(tasks);
    //}
}
