using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace UploadFileService.Application.CloudinaryFiles.Commands;
public record DeleteMultipleImageFileCommand : IRequest<Result>
{
    [Required]
    public List<string> DeleteUrls { get; init; } = null!;
}
public class DeleteMultipleImageFileCommandHandler : IRequestHandler<DeleteMultipleImageFileCommand, Result>
{
    private readonly Cloudinary _cloudinary;
    private readonly IFileUtility _fileUtility;

    public DeleteMultipleImageFileCommandHandler(Cloudinary cloudinary, IFileUtility fileUtility)
    {
        _cloudinary = cloudinary;
        _fileUtility = fileUtility;
    }

    public async Task<Result> Handle(DeleteMultipleImageFileCommand request, CancellationToken cancellationToken)
    {
        var deleteUrls = request.DeleteUrls;

        if (deleteUrls == null || !deleteUrls.Any())
        {
            return Result.Failure(CloudinaryFileError.NotFound);
        }

        var listPublicId = new List<string?>();
        foreach (var url in deleteUrls)
        {
            listPublicId.Add(_fileUtility.GetPublicIdByUrl(url));
        }

        if(listPublicId.Count != deleteUrls.Count)
        {
            return Result.Failure(CloudinaryFileError.FileListContainNull);
        }
        try {
            var deleteResults = Enumerable.Repeat(new DeletionResult(), listPublicId.Count).ToList();
            Result? fileError = null;
            var tasks = listPublicId.Select(async (publicId, index) =>
            {
                if (publicId == null)
                {
                    fileError = CloudinaryFileError.NotFound;
                    return;
                }
                var deleteParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Image,
                };
                var deleteResult = await _cloudinary.DestroyAsync(deleteParams);
                deleteResults[index] = deleteResult;
                return;
            }).ToList();

            await Task.WhenAll(tasks);
            if (fileError != null) return Result.Failure(CloudinaryFileError.DeleteToCloudFail);
            if (deleteResults.Any(result => result != null && result.StatusCode != HttpStatusCode.OK)) return Result.Failure(CloudinaryFileError.DeleteToCloudFail);
            return Result.Success();
        }
        catch (Exception ex) {
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result.Failure(CloudinaryFileError.DeleteToCloudFail);
        }
    }
}
