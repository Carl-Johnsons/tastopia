using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UploadFileService.Application.Files.Commands;
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
        try
        {
            var deleteResults = Enumerable.Repeat(new DeletionResult(), listPublicId.Count).ToList();
            var tasks = listPublicId.Select(async (publicId, index) =>
            {
                if (string.IsNullOrEmpty(publicId))
                {
                    await Console.Out.WriteLineAsync($"Not have permission to delete, so skip 1 image!");
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
            return Result.Success();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result.Failure(CloudinaryFileError.DeleteToCloudFail);
        }
    }
}
