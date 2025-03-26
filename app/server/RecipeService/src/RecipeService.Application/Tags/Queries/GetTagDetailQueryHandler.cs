using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
namespace RecipeService.Application.Tags.Queries;
public class GetTagDetailQuery: IRequest<Result<Tag?>>
{
    public Guid TagId { get; set; }
}

public class GetTagDetailQueryHandler : IRequestHandler<GetTagDetailQuery, Result<Tag?>>
{
    private readonly IApplicationDbContext _context;

    public GetTagDetailQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Tag?>> Handle(GetTagDetailQuery request, CancellationToken cancellationToken)
    {
        var tagId = request.TagId;
        if(tagId == Guid.Empty)
        {
            return Result<Tag?>.Failure(TagError.NullParameter, "TagId is null.");
        }
        var tag = await _context.Tags.SingleOrDefaultAsync(t => t.Id == tagId);
        if (tag == null)
        {
            return Result<Tag?>.Failure(TagError.NotFound, "Not found tag.");
        }
        return Result<Tag?>.Success(tag);
    }
}
