using Contract.Event.NotificationEvent;
using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;
using System;

namespace PostService.Application.Comments.Commands;

public class CreateReplyCommand : IRequest<Result>
{
    public Guid CommentId { get; init; }
    public Guid ReplyCommentId { get; init; }
}


public class CreateReplyCommandHandler : IRequestHandler<CreateReplyCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;

    public CreateReplyCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(CreateReplyCommand request, CancellationToken cancellationToken)
    {
        var comment = _context.Comments
                        .Where(c => c.Id == request.CommentId)
                        .SingleOrDefault();

        var replyComment = _context.Comments
                        .Where(c => c.Id == request.ReplyCommentId)
                        .SingleOrDefault();

        if (comment == replyComment)
        {
            return Result.Failure(CommentError.CantNotDuplicate);
        }

        if (comment != null && replyComment != null)
        {
            _context.CommentReplies.Add(new CommentReplies
            {
                CommentId = comment.Id,
                ReplyCommentId = replyComment.Id
            });

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            await _serviceBus.Publish<CreateNotificationEvent>(new CreateNotificationEvent
            {
                ActionCode = "POST_REPLY",
                ActorIds = [replyComment.UserId],
                CategoryCode = "POST",
                Url = "",
                OwnerId = comment.UserId,
            });

            return Result.Success();            
        }
        else
        {
            return Result.Failure(CommentError.NotFound);
        }

    }
}
