using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace UploadFileService.Application.CloudinaryFiles.Commands;
public record UpdateMultipleCloudinaryImageFileCommand : IRequest<Result<List<CloudinaryFile>?>>
{
    public List<string>? Urls { get; init; } = null!;
    public List<IFormFile>? FormFiles { get; init; } = null!;
}
public class UpdateMultipleCloudinaryImageFileCommandHandler : IRequestHandler<UpdateMultipleCloudinaryImageFileCommand, Result<List<CloudinaryFile>?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileUtility _fileUtility;
    private readonly Cloudinary _cloudinary;

    public UpdateMultipleCloudinaryImageFileCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, Cloudinary cloudinary, IFileUtility fileUtility)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _cloudinary = cloudinary;
        _fileUtility = fileUtility;
    }

    public async Task<Result<List<CloudinaryFile>?>> Handle(UpdateMultipleCloudinaryImageFileCommand request, CancellationToken cancellationToken)
    {
        var formFiles = request.FormFiles;
        var urls = request.Urls;
        var result = new List<CloudinaryFile>();

        if (formFiles != null && formFiles.Any())
        {
            var upload = await UploadMultipleImage(formFiles, cancellationToken);
            result = upload.Value;
            if (upload.IsFailure) return upload;
        }

        if (urls != null && urls.Any())
        {
            await DeleteMultipleImage(urls);
        }

        if((formFiles == null || !formFiles.Any()) && (urls == null || !urls.Any()))
        {
            return Result<List<CloudinaryFile>?>.Failure(CloudinaryFileError.NotFound);
        }

        return Result<List<CloudinaryFile>?>.Success(result);
    }

    private async Task<Result<List<CloudinaryFile>?>> UploadMultipleImage(List<IFormFile> formFiles, CancellationToken cancellationToken)
    {
        foreach (var file in formFiles)
        {
            if (file == null)
            {
                return Result<List<CloudinaryFile>?>.Failure(CloudinaryFileError.FileListContainNull);
            }
            if (file.Length > 10 * 1024 * 1024)
            {
                return Result<List<CloudinaryFile>?>.Failure(CloudinaryFileError.FileListContainLarge(10 * 1024 * 1024, file.Length));
            }
        }

        var returnResult = Enumerable.Repeat(new CloudinaryFile(), formFiles.Count).ToList();
        var uploadResults = Enumerable.Repeat(new ImageUploadResult(), formFiles.Count).ToList();
        var formFileNames = Enumerable.Repeat(string.Empty, formFiles.Count).ToList();
        var formFileSizes = Enumerable.Repeat(0L, formFiles.Count).ToList();
        var extensionValues = Enumerable.Repeat(string.Empty, formFiles.Count).ToList();
        var extensionTypes = Enumerable.Repeat(new ExtensionType(), formFiles.Count).ToList();
        //Used to store new extension type that not in DB
        List<ExtensionType> newExtensionTypes = new List<ExtensionType>();

        Result? cloudinaryFileError = null;
        var tasks = formFiles.Select(async (formFile, index) =>
        {
            string fileName = formFile.FileName;
            Stream fileStream = formFile.OpenReadStream();
            formFileNames[index] = fileName;
            formFileSizes[index] = fileStream.Length;
            extensionValues[index] = Path.GetExtension(fileName);
            if (_fileUtility.GetFileType(fileName) != IFileUtility.FileType.IMAGE)
            {
                cloudinaryFileError = CloudinaryFileError.InvalidFile("Image", Path.GetExtension(fileName));
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
        //check if has error in tasks
        if (cloudinaryFileError != null) return Result<List<CloudinaryFile>?>.Failure(cloudinaryFileError.Errors);
        //check if all image upload to cloud susscess
        if (uploadResults.Any(result => result.StatusCode != HttpStatusCode.OK))
        {
            RollBackCloudinary(uploadResults);
            return Result<List<CloudinaryFile>?>.Failure(CloudinaryFileError.UploadToCloudFail);
        }
        await Console.Out.WriteLineAsync("Upload result count:" + uploadResults.Count);
        for (int i = 0; i < uploadResults.Count; i++)
        {
            var uploadResult = uploadResults[i];
            string publicId = uploadResult.PublicId;
            string name = formFileNames[i];
            long size = formFileSizes[i];
            string url = uploadResult.Url.ToString();
            string extensionValue = Path.GetExtension(name);

            var extensionType = await _context.ExtensionTypes.Where(et => et.Value == extensionValue)
                .SingleOrDefaultAsync(cancellationToken);
            //extensionValues.Take(i).All(et => et != extensionValue) return true if this extensionValue appear first in the list
            // ex: LIST: {A B C D B A} index 1 = B will return true, index 4 = B return false
            //This condition to remove needless _context.ExtensionTypes.Add(extensionType);
            if (extensionType == null && extensionValues.Take(i).All(et => et != extensionValue))
            {
                extensionType = new ExtensionType
                {
                    Value = extensionValue,
                    Code = extensionValue.Replace(".", "").ToUpper(),
                    Type = _fileUtility.GetFileType(name).ToString(),
                };
                newExtensionTypes.Add(extensionType);
                _context.ExtensionTypes.Add(extensionType);
            }
            var cloudinaryFile = new CloudinaryFile
            {
                Name = name,
                PublicId = publicId,
                Size = size,
                Url = url,
                ExtensionTypeId = extensionType != null ? extensionType.Id :
                newExtensionTypes.FirstOrDefault(et => et.Value == extensionValues[i])!.Id,
            };
            _context.CloudinaryFiles.Add(cloudinaryFile);
            returnResult[i] = cloudinaryFile;
            extensionTypes[i] = extensionType!;
        }
        await _unitOfWork.SaveChangeAsync(cancellationToken);
        await Console.Out.WriteLineAsync("return result count:" + returnResult.Count);
        return Result<List<CloudinaryFile>?>.Success(returnResult);
    }

    private async Task DeleteMultipleImage(List<string> urls)
    {
        var setPublicId = new HashSet<string?>();
        foreach (var url in urls)
        {
            setPublicId.Add(_fileUtility.GetPublicIdByUrl(url));
            await Console.Out.WriteLineAsync(_fileUtility.GetPublicIdByUrl(url));
        }
        var deleteResults = Enumerable.Repeat(new DeletionResult(), setPublicId.Count).ToList();
        Result? cloudinaryFileError = null;
        var tasks = setPublicId.Select((publicId, index) =>
        {
            if (publicId == null || publicId == "")
            {
                cloudinaryFileError = CloudinaryFileError.NotFound;
                return Task.CompletedTask;
            }

            var deleteParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image,
            };
            var deleteResult = _cloudinary.Destroy(deleteParams);
            deleteResults[index] = deleteResult;
            return Task.CompletedTask;
        }).ToList();
        await Task.WhenAll(tasks);
        var removeList = _context.CloudinaryFiles.Where(c => setPublicId.Contains(c.PublicId)).ToList();
        _context.CloudinaryFiles.RemoveRange(removeList);
    }


    private async void RollBackCloudinary(List<ImageUploadResult> uploadResults)
    {
        var tasks = uploadResults.Select(async (result, index) =>
        {
            var deleteParams = new DeletionParams(result.PublicId)
            {
                ResourceType = ResourceType.Image,
            };
            var deleteResult = await _cloudinary.DestroyAsync(deleteParams);
        }).ToList();
        await Task.WhenAll(tasks);
    }
}
