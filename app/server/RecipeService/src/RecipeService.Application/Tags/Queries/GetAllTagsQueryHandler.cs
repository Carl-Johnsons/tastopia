using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
namespace RecipeService.Application.Tags.Queries;

public class GetAllTagsQuery : IRequest<Result<List<Tag>>>
{
}

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, Result<List<Tag>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllTagsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Tag>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _context.Tags.ToListAsync();
        if(tags == null || tags.Count == 0)
        {
            return Result<List<Tag>>.Success([]);
        }
        return Result<List<Tag>>.Success(tags!);
    }
}
