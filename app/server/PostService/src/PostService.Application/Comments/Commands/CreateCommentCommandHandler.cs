using Contract.Event.NotificationEvent;
using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Comments.Commands;

public class CreateCommentCommand : IRequest<Result<Comment>>
{
    public string Content { get; init; } = null!;
    public Guid UserId { get; init; }

    public Guid PostId { get; init; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result<Comment>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;

    public CreateCommentCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result<Comment>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {

        var post = _context.Posts
                    .Where(p => p.Id == request.PostId)
                    .SingleOrDefault();

        if (post == null)
        {
            return Result<Comment>.Failure(PostError.NotFound)!;
        }

        Comment comment = new Comment
        {
            Content = request.Content,
            UserId = request.UserId
        };

        _context.Comments.Add(comment);


        PostComment postCommment = new PostComment
        {
            PostId = request.PostId,
            CommentId = comment.Id
        };

        _context.PostComments.Add(postCommment);

        await _unitOfWork.SaveChangeAsync();

        await _serviceBus.Publish<CreateNotificationEvent>(new CreateNotificationEvent
        {
            ActionCode = "POST_COMMENT",
            ActorIds = [request.UserId],
            CategoryCode = "POST",
            Url = "",
            OwnerId = post.UserId,
        });

        return Result<Comment>.Success(comment);
    }
}
