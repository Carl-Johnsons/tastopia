using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.Entities;
using PostService.Domain.Errors;

namespace PostService.Application.Posts.Queries;

public class GetPostReportByPostIdCommand : IRequest<Result<List<PostReport>?>>
{
    public Guid PostId { get; init; }
}

public class GetPostReportByPostIdCommandHandler : IRequestHandler<GetPostReportByPostIdCommand, Result<List<PostReport>?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public GetPostReportByPostIdCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<PostReport>?>> Handle(GetPostReportByPostIdCommand request, CancellationToken cancellationToken)
    {
        var posts = await _context.PostReports
                    .Where(p => p.PostId == request.PostId)
                    .Include(p => p.Post)
                    .ToListAsync(cancellationToken);

        if (posts == null )
        {
            return Result<List<PostReport>>.Failure(PostError.NotFound);
        }

        return Result<List<PostReport>?>.Success(posts);
    }
}
