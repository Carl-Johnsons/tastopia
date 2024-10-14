using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace UploadFileService.Application.CloudinaryFiles.Commands;
public record UpdateCloudinaryImageFileCommand : IRequest<Result<CloudinaryFile?>>
{
    [Required]
    public string Url { get; set; } = null!;
    [Required]
    public IFormFile FormFile { get; init; } = null!;
}
public class UpdateCloudinaryImageFileCommandHandler : IRequestHandler<UpdateCloudinaryImageFileCommand, Result<CloudinaryFile?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileUtility _fileUtility;
    private readonly Cloudinary _cloudinary;

    public UpdateCloudinaryImageFileCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, Cloudinary cloudinary, IFileUtility fileUtility)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _cloudinary = cloudinary;
        _fileUtility = fileUtility;
    }

    public async Task<Result<CloudinaryFile?>> Handle(UpdateCloudinaryImageFileCommand request, CancellationToken cancellationToken)
    {
        CloudinaryFile? cloudinaryFile;
        ExtensionType? extensionType;
        var formFile = request.FormFile;
        var Url = request.Url;
        if (formFile == null || formFile.Length == 0)
        {
            return Result<CloudinaryFile?>.Failure(CloudinaryFileError.NotFound);
        }

        string fileName = formFile.FileName;
        Stream fileStream = formFile.OpenReadStream();

        if (_fileUtility.getFileType(fileName) != "Image")
        {
            await Console.Out.WriteLineAsync("okkkkkkkkkkkkkkkkkkkkkkkkkkkkk if going:" + _fileUtility.getFileType(fileName));
            return Result<CloudinaryFile?>.Failure(CloudinaryFileError.InvalidFile("Image", Path.GetExtension(fileName)));
        }

        cloudinaryFile = await _context.CloudinaryFiles.Where(cf => cf.Url == Url).Include(cf => cf.ExtensionType).SingleOrDefaultAsync();
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(fileName, fileStream),
            PublicId = cloudinaryFile != null ? cloudinaryFile.PublicId : Guid.NewGuid().ToString(),
            //PublicId = Guid.NewGuid().ToString() + fileName.Substring(0, fileName.LastIndexOf(".")),
            Folder = cloudinaryFile != null ? "" : Environment.GetEnvironmentVariable("Cloudinary_Folder"),
        };
        await Console.Out.WriteLineAsync("Going upload file to cloud");
        await Console.Out.WriteLineAsync(uploadParams.Folder);
        await Console.Out.WriteLineAsync(uploadParams.PublicId);
        await Console.Out.WriteLineAsync(uploadParams.File.FileName);



        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        if (uploadResult.StatusCode == HttpStatusCode.OK)
        {
            string publicId = uploadResult.PublicId;
            string name = fileName;
            long size = formFile.Length;
            string url = uploadResult.Url.ToString();
            string extensionValue = Path.GetExtension(fileName);
            extensionType = await _context.ExtensionTypes.Where(et => et.Value == extensionValue)
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
            if (cloudinaryFile != null)
            {
                cloudinaryFile.Name = name;
                cloudinaryFile.PublicId = publicId;
                cloudinaryFile.Size = size;
                cloudinaryFile.Url = url;
                cloudinaryFile.ExtensionTypeId = extensionType.Id;
                _context.CloudinaryFiles.Update(cloudinaryFile);
            }
            else
            {
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

        }
        else
        {
            return Result<CloudinaryFile?>.Failure(CloudinaryFileError.UploadToCloudFail);

        }
        await _unitOfWork.SaveChangeAsync(cancellationToken);
        cloudinaryFile.ExtensionType = extensionType;
        return Result<CloudinaryFile?>.Success(cloudinaryFile);
    }
}
