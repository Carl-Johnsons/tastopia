using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;

namespace PostService.Application.Tags.Queries;

public record GetAllTagsQuery : IRequest<Result<List<Tag>>>;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, Result<List<Tag>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllTagsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Tag>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _context.Tags
                    .OrderBy(t => t.CreatedAt)
                    .ToListAsync(cancellationToken);

        return Result<List<Tag>>.Success(tags);
    }
}
