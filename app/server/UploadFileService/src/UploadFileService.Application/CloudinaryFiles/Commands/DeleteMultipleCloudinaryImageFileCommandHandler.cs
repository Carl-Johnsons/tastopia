using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace UploadFileService.Application.CloudinaryFiles.Commands;
public record DeleteMultipleCloudinaryImageFileCommand : IRequest<Result>
{
    [Required]
    public List<Guid?> FileIds { get; init; } = null!;
}
public class DeleteMultipleCloudinaryImageFileCommandHandler : IRequestHandler<DeleteMultipleCloudinaryImageFileCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Cloudinary _cloudinary;

    public DeleteMultipleCloudinaryImageFileCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, Cloudinary cloudinary)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _cloudinary = cloudinary;
    }

    public async Task<Result> Handle(DeleteMultipleCloudinaryImageFileCommand request, CancellationToken cancellationToken)
    {

        var fileIds = request.FileIds;

        if (fileIds == null || !fileIds.Any())
        {
            await Console.Out.WriteLineAsync("The file list cannot be null or empty.command");
            await Console.Out.WriteLineAsync("form file count:" + fileIds?.Count);
            return CloudinaryFileError.NotFound;
        }
        if (fileIds.Any(Id => Id == null)) return CloudinaryFileError.FileListContainNull;
        var deleteResults = Enumerable.Repeat(new DeletionResult(), fileIds.Count).ToList();
        var cloudinaryFiles = new List<CloudinaryFile?>();
        foreach (var Id in fileIds)
        {
            var cloudinaryFile = await _context.CloudinaryFiles.Where(cf => cf.Id == Id).Include(cf => cf.ExtensionType).SingleOrDefaultAsync();
            cloudinaryFiles.Add(cloudinaryFile);
        }
        await Console.Out.WriteLineAsync("FormFile count command:" + fileIds.Count);
        Result? cloudinaryFileError = null;
        var tasks = cloudinaryFiles.Select(async (cloudinaryFile, index) =>
        {
            if (cloudinaryFile == null || cloudinaryFile.PublicId == null)
            {
                cloudinaryFileError = CloudinaryFileError.NotFound;
                return;
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
                    cloudinaryFileError = CloudinaryFileError.InvalidFile("Image, Video, Raw", Path.GetExtension(cloudinaryFile.Name));
                    return;
            }
            var deleteParams = new DeletionParams(cloudinaryFile.PublicId)
            {
                ResourceType = rst,
            };
            var deleteResult = _cloudinary.Destroy(deleteParams);
            deleteResults[index] = deleteResult;
        }).ToList();
        await Task.WhenAll(tasks);
        //check if has error in tasks
        if (cloudinaryFileError != null) return cloudinaryFileError;
        //check if all image delete to cloud susscess
        if (deleteResults.Any(result => result.StatusCode != HttpStatusCode.OK)) return CloudinaryFileError.DeleteToCloudFail;
        await Console.Out.WriteLineAsync("Upload result count:" + deleteResults.Count);
        for (int i = 0; i < deleteResults.Count; i++)
        {
            await Console.Out.WriteLineAsync("deleteok!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            _context.CloudinaryFiles.Remove(cloudinaryFiles[i]!);
        }
        await _unitOfWork.SaveChangeAsync(cancellationToken);
        return Result.Success();
    }
}
