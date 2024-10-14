using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Interactions.Queries;

public class GetInteractionByPostIdQuery : IRequest<Result<List<Interaction>?>>
{
    public Guid PostId { get; init; }
    public Guid? UserId { get; init; }
}

public class GetInteractionByPostIdQueryHandler : IRequestHandler<GetInteractionByPostIdQuery, Result<List<Interaction>?>>
{
    private readonly IApplicationDbContext _context;

    public GetInteractionByPostIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Interaction>?>> Handle(GetInteractionByPostIdQuery request, CancellationToken cancellationToken)
    {
        var post = _context.Posts
                    .Where(p => p.Id == request.PostId)
                    .FirstOrDefault();

        if (post == null)
        {
            return Result<List<Interaction>?>.Failure(PostError.NotFound);
        }

        var query = _context.PostInteracts
                                .Where(i => i.PostId == request.PostId)
                                .AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(i => i.UserId == request.UserId);
        }

        var interactions = await query.Include(pt => pt.Interaction).
                                Select(pt => pt.Interaction).ToListAsync();

        return Result<List<Interaction>?>.Success(interactions);
    }
}
