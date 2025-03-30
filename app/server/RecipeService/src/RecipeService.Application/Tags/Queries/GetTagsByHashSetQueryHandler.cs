using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
namespace RecipeService.Application.Tags.Queries;

public record GetTagsByHashSetQuery : IRequest<Result<Dictionary<Guid, Tag>>>
{
    public HashSet<Guid> TagIds { get; init; } = null!;
}

public class GetTagsByHashSetQueryHandler : IRequestHandler<GetTagsByHashSetQuery, Result<Dictionary<Guid, Tag>>>
{
    private readonly IApplicationDbContext _context;

    public GetTagsByHashSetQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Dictionary<Guid, Tag>>> Handle(GetTagsByHashSetQuery request,
                                                CancellationToken cancellationToken)
    {
        var tags = await _context.Tags
            .Where(t => request.TagIds.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, t => t, cancellationToken);

        if (tags == null || tags.Count == 0)
        {
            return Result<Dictionary<Guid, Tag>>.Success([]);
        }
        return Result<Dictionary<Guid, Tag>>.Success(tags!);
    }
}
