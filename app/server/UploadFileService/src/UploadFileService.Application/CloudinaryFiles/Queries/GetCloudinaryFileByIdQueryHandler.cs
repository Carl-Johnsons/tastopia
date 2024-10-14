using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UploadFileService.Application.CloudinaryFiles.Queries;

public record GetCloudinaryFileByIdQuery : IRequest<Result<CloudinaryFile?>>
{
    [Required]
    public Guid? Id { get; init; }
}
public class GetCloudinaryFileByIdQueryHandler : IRequestHandler<GetCloudinaryFileByIdQuery, Result<CloudinaryFile?>>
{
    private readonly IApplicationDbContext _context;

    public GetCloudinaryFileByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CloudinaryFile?>> Handle(GetCloudinaryFileByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.CloudinaryFiles.Where(cf => cf.Id == request.Id).Include(cf => cf.ExtensionType).SingleOrDefaultAsync(cancellationToken);
        return Result<CloudinaryFile?>.Success(result);
    }
}
