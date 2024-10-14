using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace UploadFileService.Application.CloudinaryFiles.Commands;
public record CreateMultipleCloudinaryImageFileCommand : IRequest<Result<List<CloudinaryFile>?>>
{
    [Required]
    public List<IFormFile> FormFiles { get; init; } = null!;
}
public class CreateMultipleCloudinaryImageFileCommandHandler : IRequestHandler<CreateMultipleCloudinaryImageFileCommand, Result<List<CloudinaryFile>?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileUtility _fileUtility;
    private readonly Cloudinary _cloudinary;

    public CreateMultipleCloudinaryImageFileCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, Cloudinary cloudinary, IFileUtility fileUtility)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _cloudinary = cloudinary;
        _fileUtility = fileUtility;
    }

    public async Task<Result<List<CloudinaryFile>?>> Handle(CreateMultipleCloudinaryImageFileCommand request, CancellationToken cancellationToken)
    {
        var formFiles = request.FormFiles;

        if (formFiles == null || !formFiles.Any())
        {
            await Console.Out.WriteLineAsync("The file list cannot be null or empty.command");
            await Console.Out.WriteLineAsync("form file count:" + formFiles?.Count);
            return Result<List<CloudinaryFile>?>.Failure(CloudinaryFileError.NotFound);
        }

        if (formFiles.Any(file => file == null)) return Result<List<CloudinaryFile>?>.Failure(CloudinaryFileError.FileListContainNull);


        var returnResult = Enumerable.Repeat(new CloudinaryFile(), formFiles.Count).ToList();
        var uploadResults = Enumerable.Repeat(new ImageUploadResult(), formFiles.Count).ToList();
        var formFileNames = Enumerable.Repeat(string.Empty, formFiles.Count).ToList();
        var formFileSizes = Enumerable.Repeat(0L, formFiles.Count).ToList();
        var extensionValues = Enumerable.Repeat(string.Empty, formFiles.Count).ToList();
        var extensionTypes = Enumerable.Repeat(new ExtensionType(), formFiles.Count).ToList();

        //Used to store new extension type that not in DB
        List<ExtensionType> newExtensionTypes = new List<ExtensionType>();

        await Console.Out.WriteLineAsync("FormFile count command:" + formFiles.Count);
        Result? cloudinaryFileError = null;
        var tasks = formFiles.Select(async (formFile, index) =>
        {
            await Console.Out.WriteLineAsync("asyn index:" + index);
            string fileName = formFile.FileName;
            Stream fileStream = formFile.OpenReadStream();
            formFileNames[index] = fileName;
            formFileSizes[index] = fileStream.Length;
            extensionValues[index] = Path.GetExtension(fileName);
            if (_fileUtility.getFileType(fileName) != "Image")
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
        ;
        //check if all image upload to cloud susscess
        if (uploadResults.Any(result => result.StatusCode != HttpStatusCode.OK)) return Result<List<CloudinaryFile>?>.Failure(CloudinaryFileError.UploadToCloudFail);
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
                    Type = _fileUtility.getFileType(name).ToUpper(),
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
}
