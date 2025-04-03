using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
namespace RecipeService.Application.Tags.Queries;
public class GetTagDetailQuery: IRequest<Result<TagResponse?>>
{
    public Guid TagId { get; set; }
}

public class GetTagDetailQueryHandler : IRequestHandler<GetTagDetailQuery, Result<TagResponse?>>
{
    private readonly IApplicationDbContext _context;

    public GetTagDetailQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<TagResponse?>> Handle(GetTagDetailQuery request, CancellationToken cancellationToken)
    {
        var tagId = request.TagId;
        if(tagId == Guid.Empty)
        {
            return Result<TagResponse?>.Failure(TagError.NullParameter, "TagId is null.");
        }
        var tag = await _context.Tags.SingleOrDefaultAsync(t => t.Id == tagId);
        if (tag == null)
        {
            return Result<TagResponse?>.Failure(TagError.NotFound, "Not found tag.");
        }
        return Result<TagResponse?>.Success(new TagResponse
        {
            Id = tag.Id,
            Category = tag.Category.ToString(),
            Code = tag.Code,
            Vi = tag.Value.Vi,
            En = tag.Value.En,
            CreatedAt = tag.CreatedAt,
            ImageUrl = tag.ImageUrl,
            Status = tag.Status.ToString()
        });
    }
}
