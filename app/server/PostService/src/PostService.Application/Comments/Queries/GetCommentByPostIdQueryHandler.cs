using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.Constants;
using PostService.Domain.DTOs;
using PostService.Domain.Errors;

namespace PostService.Application.Comments.Queries;

public class GetCommentByPostIdQuery : BasePaginatedDTO, IRequest<Result<PaginatedCommentListResponseDTO?>>
{
    public Guid PostId { get; init; }
}

public class GetCommentByPostIdQueryHandler : IRequestHandler<GetCommentByPostIdQuery, Result<PaginatedCommentListResponseDTO?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<PostComment, EmptyMetadata> _paginateDataUtility;

    public GetCommentByPostIdQueryHandler(IApplicationDbContext context, IPaginateDataUtility<PostComment, EmptyMetadata> paginateDataUtility)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedCommentListResponseDTO?>> Handle(GetCommentByPostIdQuery request, CancellationToken cancellationToken)
    {
        var post = _context.Posts
                            .Where(p => p.Id == request.PostId)
                            .SingleOrDefault();



        if (post == null)
        {
            return Result<PaginatedCommentListResponseDTO>.Failure(PostError.NotFound);
        }

        var commentsQuery = _context.PostComments
                            .Where(pc => pc.PostId == request.PostId)
                            .OrderByDescending(pc => pc.Comment.CreatedAt)
                            .AsQueryable();

        commentsQuery = _paginateDataUtility.PaginateQuery(commentsQuery, new PaginateParam
        {
            Offset = request.Skip * POST_CONSTANTS.COMMENT_LIMIT,
            Limit = POST_CONSTANTS.COMMENT_LIMIT
        });

        var comments = await commentsQuery
                            .Include(pc => pc.Comment)
                            .Select(pc => pc.Comment)
                            .ToListAsync();

        var response = new PaginatedCommentListResponseDTO
        {
            PaginatedData = comments,
            Metadata = new EmptyMetadata()
        };

        return Result<PaginatedCommentListResponseDTO?>.Success(response);
    }
}
