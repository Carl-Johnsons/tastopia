using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.Constants;
using PostService.Domain.DTOs;

namespace PostService.Application.Posts.Queries;

public class GetAllPostsQuery : BasePaginatedDTO, IRequest<Result<PaginatedPostListResponseDTO>>
{

};

public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, Result<PaginatedPostListResponseDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Post, PostListMetadata> _paginateDataUtility;

    public GetAllPostsQueryHandler(IApplicationDbContext context, IPaginateDataUtility<Post, PostListMetadata> paginateDataUtility)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedPostListResponseDTO>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var postQuery = _context.Posts.OrderByDescending(p => p.CreatedAt).AsQueryable();

        postQuery = _paginateDataUtility.PaginateQuery(postQuery, new PaginateParam
        {
            Offset = request.Skip * POST_CONSTANTS.LIMIT,
            Limit = POST_CONSTANTS.LIMIT
        });

        var postIds = await postQuery.Select(p=>p.Id.ToString()).ToListAsync();

        //List<PostDTO> result = new List<PostDTO>();

        //foreach (var p in posts)
        //{
        //    var post = await _context.Posts
        //                .Where(pt => pt.Id == p.Id)
        //                .SingleOrDefaultAsync();

        //    var tag = await _context.PostTags
        //                .Where(t => t.PostId == p.Id)
        //                .Include(t => t.Tag)
        //                .ToListAsync();

        //    var countInteract = await _context.PostInteracts
        //                .Where(pi => pi.PostId == p.Id)
        //                .CountAsync();

        //    List<string> tags = new List<string>();

        //    foreach (PostTag t in tag)
        //    {
        //        tags.Add(t.Tag.Value);
        //    }

        //    var topInteractions = await _context.PostInteracts
        //            .Where(pi => pi.PostId == p.Id)
        //            .GroupBy(pi => pi.InteractionId)
        //            .Select(g => new PostInteractionCountDTO
        //            {
        //                InteractionId = g.Key,
        //                Count = g.Count(),
        //                Value = _context.Interactions.FirstOrDefault(iv => iv.Id == g.Key)!.Value
        //            })
        //            .OrderByDescending(pi => pi.Count)
        //            .Take(3)
        //            .ToListAsync();

        //    List<string> interacts = new List<string>();

        //    foreach (var t in topInteractions)
        //    {
        //        interacts.Add(t.Value);
        //    }
        //    if (post != null)
        //    {
        //        var postReponse = new PostDTO
        //        {
        //            Id = p.Id,
        //            Content = post.Content,
        //            UserId = post.UserId,
        //            CreatedAt = post.CreatedAt,
        //            InteractTotal = countInteract,
        //            Interactions = interacts,
        //            Tags = tags
        //        };

        //        result.Add(postReponse);
        //    }
        //}

        var paginatedResponse = new PaginatedPostListResponseDTO
        {
            PaginatedData = postIds,
            Metadata = new PostListMetadata
            {
            }
        };

        return Result<PaginatedPostListResponseDTO>.Success(paginatedResponse);
    }
}
