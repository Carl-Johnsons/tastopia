using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.DTOs;
using PostService.Domain.Errors;
using Sprache;
using static MassTransit.ValidationResultExtensions;
namespace PostService.Application.Posts.Queries;

public class GetPostsByUserIdQuery : IRequest<Result<List<PostDTO>?>>
{
    public Guid UserId { get; init; }
}

public class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, Result<List<PostDTO>?>>
{
    private readonly IApplicationDbContext _context;

    public GetPostsByUserIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<PostDTO>?>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var posts = await _context.Posts
                        .Where(p => p.UserId == request.UserId)
                        .ToListAsync();

        if (posts == null)
        {
            return Result<List<PostDTO>>.Failure(PostError.UserNotFound);
        }

        List<PostDTO> result = new List<PostDTO>();

        foreach (var p in posts)
        {
            var post = await _context.Posts
                        .Where(pt => pt.Id == p.Id)
                        .SingleOrDefaultAsync();

            var tags = await _context.PostTags
                        .Where(pt => pt.PostId == p.Id)
                        .Include(pt => pt.Tag)
                        .Select(pt => pt.Tag)
                        .ToListAsync();

            var countInteract = await _context.PostInteracts
                        .Where(pi => pi.PostId == p.Id)
                        .CountAsync();


            var topInteractions = await _context.PostInteracts
                    .Where(pi => pi.PostId == p.Id)
                    .GroupBy(pi => pi.InteractionId)
                    .Select(g => new PostInteractionCountDTO
                    {
                        InteractionId = g.Key,
                        Count = g.Count(),
                        Value = _context.Interactions.FirstOrDefault(iv => iv.Id == g.Key)!.Value
                    })
                    .OrderByDescending(pi => pi.Count)
                    .Take(3)
                    .ToListAsync();

            List<string> interacts = new List<string>();

            foreach (var t in topInteractions)
            {
                interacts.Add(t.Value);
            }

            if (post != null)
            {
                var postReponse = new PostDTO
                {
                    Id = p.Id,
                    Content = post.Content,
                    UserId = post.UserId,
                    CreatedAt = post.CreatedAt,
                    InteractTotal = countInteract,
                    Interactions = interacts,
                    Tags = tags
                };

                result.Add(postReponse);
            }

        }

        return Result<List<PostDTO>?>.Success(result);
    }
}
