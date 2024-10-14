using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace UploadFileService.Application.CloudinaryFiles.Commands;
public record DeleteCloudinaryFileCommand : IRequest<Result>
{
    [Required]
    public Guid? Id { get; init; }
}
public class DeleteCloudinaryFileCommandHandler : IRequestHandler<DeleteCloudinaryFileCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Cloudinary _cloudinary;

    public DeleteCloudinaryFileCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, Cloudinary cloudinary)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _cloudinary = cloudinary;
    }

    public async Task<Result> Handle(DeleteCloudinaryFileCommand request, CancellationToken cancellationToken)
    {
        Guid? Id = request.Id;

        if (Id == null)
        {
            return CloudinaryFileError.NotFound;
        }

        var cloudinaryFile = await _context.CloudinaryFiles.Where(cf => cf.Id == Id).Include(cf => cf.ExtensionType).SingleOrDefaultAsync();


        if (cloudinaryFile == null || cloudinaryFile.PublicId == null)
        {
            return CloudinaryFileError.NotFound;

        }

        ResourceType rst;
        switch (cloudinaryFile.ExtensionType.Type)
        {
            case "IMAGE":
                rst = ResourceType.Image;
                break;
            case "VIDEO":
                rst = ResourceType.Video;
                break;
            case "RAW":
                rst = ResourceType.Raw;
                break;
            default:
                return CloudinaryFileError.InvalidFile("Image, Video, Raw", Path.GetExtension(cloudinaryFile.Name));
        }

        var deleteParams = new DeletionParams(cloudinaryFile.PublicId)
        {
            ResourceType = rst,
        };
        var deleteResult = _cloudinary.Destroy(deleteParams);

        if (deleteResult.StatusCode != HttpStatusCode.OK)
        {
            return CloudinaryFileError.DeleteToCloudFail;
        }

        await Console.Out.WriteLineAsync("deleteok!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        _context.CloudinaryFiles.Remove(cloudinaryFile);
        await _unitOfWork.SaveChangeAsync(cancellationToken);
        return Result.Success();

    }
}
