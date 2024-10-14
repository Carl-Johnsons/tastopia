using Contract.Event.NotificationEvent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.Constants;
using PostService.Domain.Errors;

namespace PostService.Application.Posts.Commands;

public record DeletePostCommand : IRequest<Result>
{
    public Guid UserId { get; set; }
    public Guid PostId { get; init; }
}

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISignalRService _signalRService;
    private readonly IServiceBus _serviceBus;

    public DeletePostCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ISignalRService signalRService, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _signalRService = signalRService;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
                        .Where(p => p.Id == request.PostId)
                        .SingleOrDefaultAsync(cancellationToken);
        if (post == null)
        {
            return Result.Failure(PostError.NotFound);
        }
        _context.Posts.Remove(post);
        await _unitOfWork.SaveChangeAsync(cancellationToken);
        await _signalRService.InvokeAction(SignalREvent.DELETE_POST);

        await _serviceBus.Publish<CreateNotificationEvent>(new CreateNotificationEvent
        {
            ActionCode = "POST_WARNING",
            ActorIds = [request.UserId],
            CategoryCode = "POST",
            Url = ""
        });

        return Result.Success();
    }
}
