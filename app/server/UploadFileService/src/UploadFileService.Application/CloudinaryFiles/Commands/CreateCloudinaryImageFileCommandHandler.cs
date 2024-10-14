using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace UploadFileService.Application.CloudinaryFiles.Commands;
public record CreateCloudinaryImageFileCommand : IRequest<Result<CloudinaryFile?>>
{
    [Required]
    public IFormFile FormFile { get; init; } = null!;
}
public class CreateCloudinaryImageFileCommandHandler : IRequestHandler<CreateCloudinaryImageFileCommand, Result<CloudinaryFile?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileUtility _fileUtility;
    private readonly Cloudinary _cloudinary;

    public CreateCloudinaryImageFileCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, Cloudinary cloudinary, IFileUtility fileUtility)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _cloudinary = cloudinary;
        _fileUtility = fileUtility;
    }

    public async Task<Result<CloudinaryFile?>> Handle(CreateCloudinaryImageFileCommand request, CancellationToken cancellationToken)
    {
        CloudinaryFile cloudinaryFile;

        var formFile = request.FormFile;

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

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(fileName, fileStream),
            PublicId = Guid.NewGuid().ToString(),
            //PublicId = Guid.NewGuid().ToString() + fileName.Substring(0, fileName.LastIndexOf(".")),
            Folder = Environment.GetEnvironmentVariable("Cloudinary_Folder"),
        };
        await Console.Out.WriteLineAsync("Going upload file to cloud");

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

        //var extensionType = request.ExtensionType;
        //extensionType.Code = extensionType.Value.Replace(".", "").ToUpper();
        //Console.WriteLine(JsonConvert.SerializeObject(extensionType));
        //_context.ExtensionTypes.Add(request.ExtensionType);

        //var result = JsonConvert.DeserializeObject(uploadResult.JsonObj.ToString());

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result<CloudinaryFile?>.Success(cloudinaryFile);
    }
}
