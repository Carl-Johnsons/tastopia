using Microsoft.EntityFrameworkCore;

namespace UploadFileService.Application.CloudinaryFiles.Queries;

public record GetAllCloudinaryFilesQuery : IRequest<Result<List<CloudinaryFile>>>
{
}
public class GetAllCloudinaryFilesQueryHandler : IRequestHandler<GetAllCloudinaryFilesQuery, Result<List<CloudinaryFile>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllCloudinaryFilesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<CloudinaryFile>>> Handle(GetAllCloudinaryFilesQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.CloudinaryFiles.Include(cf => cf.ExtensionType).ToListAsync();
        return Result<List<CloudinaryFile>>.Success(result);
    }
}
