using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace UploadFileService.Application.Utilities;

public class ApplicationFileUtility
{
    private readonly Cloudinary _cloudinary;

    public ApplicationFileUtility(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task RollBackImageFile(List<string> publicIds)
    {
        var tasks = publicIds.Select(async (publicId, index) =>
        {
            if (string.IsNullOrEmpty(publicId)) return;
            var deleteParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image,
            };
            var deleteResult = await _cloudinary.DestroyAsync(deleteParams);
        }).ToList();
        await Task.WhenAll(tasks);
    }
}