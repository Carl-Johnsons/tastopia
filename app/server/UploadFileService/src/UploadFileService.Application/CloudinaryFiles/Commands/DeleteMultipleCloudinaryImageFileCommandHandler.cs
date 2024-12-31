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
    public List<string> DeleteUrls { get; init; } = null!;
}
public class DeleteMultipleCloudinaryImageFileCommandHandler : IRequestHandler<DeleteMultipleCloudinaryImageFileCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Cloudinary _cloudinary;
    private readonly IFileUtility _fileUtility;

    public DeleteMultipleCloudinaryImageFileCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, Cloudinary cloudinary, IFileUtility fileUtility)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _cloudinary = cloudinary;
        _fileUtility = fileUtility;
    }

    public async Task<Result> Handle(DeleteMultipleCloudinaryImageFileCommand request, CancellationToken cancellationToken)
    {

        var deleteUrls = request.DeleteUrls;

        if (deleteUrls == null || !deleteUrls.Any())
        {
            return CloudinaryFileError.NotFound;
        }

        var listPublicId = new List<string?>();
        foreach (var url in deleteUrls)
        {
            listPublicId.Add(_fileUtility.GetPublicIdByUrl(url));
        }

        if(listPublicId.Count != deleteUrls.Count)
        {
            return CloudinaryFileError.FileListContainNull;
        }

        var deleteResults = Enumerable.Repeat(new DeletionResult(), listPublicId.Count).ToList();
        var cloudinaryFiles = new List<CloudinaryFile?>();
        foreach (var PublicId in listPublicId)
        {
            var cloudinaryFile = await _context.CloudinaryFiles.Where(cf => cf.PublicId == PublicId).Include(cf => cf.ExtensionType).SingleOrDefaultAsync();
            cloudinaryFiles.Add(cloudinaryFile);
        }
        Result? cloudinaryFileError = null;
        var tasks = listPublicId.Select((publicId, index) =>
        {
            if (publicId == null)
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
        //check if has error in tasks
        if (cloudinaryFileError != null) return cloudinaryFileError;
        //check if all image delete to cloud susscess
        if (deleteResults.Any(result => result.StatusCode != HttpStatusCode.OK)) return CloudinaryFileError.DeleteToCloudFail;
        for (int i = 0; i < deleteResults.Count; i++)
        {
            _context.CloudinaryFiles.Remove(cloudinaryFiles[i]!);
        }
        await _unitOfWork.SaveChangeAsync(cancellationToken);
        return Result.Success();
    }
}
