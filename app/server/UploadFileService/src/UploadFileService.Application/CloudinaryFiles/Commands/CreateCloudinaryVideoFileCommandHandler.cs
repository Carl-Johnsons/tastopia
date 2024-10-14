using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace UploadFileService.Application.CloudinaryFiles.Commands;
public record CreateCloudinaryVideoFileCommand : IRequest<Result<CloudinaryFile?>>
{
    [Required]
    public IFormFile FormFile { get; init; } = null!;
}
public class CreateCloudinaryVideoFileCommandHandler : IRequestHandler<CreateCloudinaryVideoFileCommand, Result<CloudinaryFile?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileUtility _fileUtility;
    private readonly Cloudinary _cloudinary;

    public CreateCloudinaryVideoFileCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, Cloudinary cloudinary, IFileUtility fileUtility)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _cloudinary = cloudinary;
        _fileUtility = fileUtility;
    }

    public async Task<Result<CloudinaryFile?>> Handle(CreateCloudinaryVideoFileCommand request, CancellationToken cancellationToken)
    {
        CloudinaryFile cloudinaryFile;

        var formFile = request.FormFile;

        if (formFile == null || formFile.Length == 0)
        {
            return Result<CloudinaryFile?>.Failure(CloudinaryFileError.NotFound);
        }

        string fileName = formFile.FileName;
        Stream fileStream = formFile.OpenReadStream();

        if (_fileUtility.getFileType(fileName) != "Video")
        {
            return Result<CloudinaryFile?>.Failure(CloudinaryFileError.InvalidFile("Video", Path.GetExtension(fileName)));
        }

        var uploadParams = new VideoUploadParams()
        {
            File = new FileDescription(fileName, fileStream),
            PublicId = Guid.NewGuid().ToString(),
            Folder = Environment.GetEnvironmentVariable("Cloudinary_Folder"),
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        if (uploadResult.StatusCode == HttpStatusCode.OK)
        {
            string publicId = uploadResult.PublicId;
            string name = fileName;
            long size = formFile.Length;
            string url = uploadResult.Url.ToString();
            string extensionValue = Path.GetExtension(fileName);
            var extensionType = await _context.ExtensionTypes.Where(et => et.Value == extensionValue)
                .SingleOrDefaultAsync(cancellationToken);
            if (extensionType == null)
            {
                extensionType = new ExtensionType
                {
                    Value = extensionValue,
                    Code = extensionValue.Replace(".", "").ToUpper(),
                    Type = _fileUtility.getFileType(fileName).ToUpper(),
                };
                _context.ExtensionTypes.Add(extensionType);
            }
            cloudinaryFile = new CloudinaryFile
            {
                Name = name,
                PublicId = publicId,
                Size = size,
                Url = url,
                ExtensionTypeId = extensionType.Id,
            };
            _context.CloudinaryFiles.Add(cloudinaryFile);
        }
        else
        {
            return Result<CloudinaryFile?>.Failure(CloudinaryFileError.UploadToCloudFail);
        }
        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result<CloudinaryFile?>.Success(cloudinaryFile);
    }

}
