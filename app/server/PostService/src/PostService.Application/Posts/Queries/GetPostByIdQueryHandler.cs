using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.DTOs;
using PostService.Domain.Errors;
namespace PostService.Application.Posts.Queries;

public class GetPostByIdQuery : IRequest<Result<PostDTO?>>
{
    public Guid Id { get; set; }
}

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Result<PostDTO?>>
{
    private readonly IApplicationDbContext _context;

    public GetPostByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PostDTO?>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
                    .Where(p => p.Id == request.Id)
                    .SingleOrDefaultAsync();

        if (post == null)
        {
            return Result<PostDTO>.Failure(PostError.NotFound);
        }

        var tags = await _context.PostTags
                    .Where(t => t.PostId == request.Id)
                    .Include(t => t.Tag)
                    .Select(pt => pt.Tag)
                    .ToListAsync();

        var countInteract = await _context.PostInteracts
                    .Where(pi => pi.PostId == request.Id)
                    .CountAsync();

        var topInteractions = await _context.PostInteracts
                    .Where(pi => pi.PostId == request.Id)
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

        var postReponse = new PostDTO
        {
            Id = request.Id,
            Content = post.Content,
            UserId = post.UserId,
            CreatedAt = post.CreatedAt,
            InteractTotal = countInteract,
            Interactions = interacts,
            Tags = tags,
            AttachedFilesURL = post.AttachedFilesURL
        };

        return Result<PostDTO?>.Success(postReponse);
    }
}
