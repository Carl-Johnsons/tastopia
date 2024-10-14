using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Interactions.Commands;

public class UninteractionPostCommand : IRequest<Result>
{
    public Guid PostId { get; init; }
    public Guid UserId { get; init; }
}

public class UninteractionPostCommandHandler : IRequestHandler<UninteractionPostCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UninteractionPostCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UninteractionPostCommand request, CancellationToken cancellationToken)
    {
        var post = _context.Posts
                    .Where(p => p.Id == request.PostId)
                    .FirstOrDefault();

        if (post == null)
        {
            return Result.Failure(PostError.NotFound);
        }

        var result = _context.PostInteracts
                        .Where(p => p.UserId == request.UserId && p.PostId == request.PostId)
                        .SingleOrDefault();

        if (result != null)
        {
            _context.PostInteracts.Remove(result);
            await _unitOfWork.SaveChangeAsync();
            return Result.Success();
        } else
        {
            return Result.Failure(PostError.NotInteractedPost);
        }
        

        
    }
}
