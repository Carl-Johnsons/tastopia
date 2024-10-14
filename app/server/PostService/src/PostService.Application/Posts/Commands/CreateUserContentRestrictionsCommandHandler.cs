using Contract.Event.NotificationEvent;
using MediatR;
using PostService.Domain.Common;
using PostService.Domain.Errors;

namespace PostService.Application.Posts.Commands;

public class CreateUserContentRestrictionsCommand : IRequest<Result>
{
    public Guid UserId { get; init; }
    public Guid TypeId { get; init; }
    public DateTime ExpiredAt { get; init; }
    public Guid AdminId { get; init; }
}

public class CreateUserContentRestrictionsCommandHandler : IRequestHandler<CreateUserContentRestrictionsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;

    public CreateUserContentRestrictionsCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(CreateUserContentRestrictionsCommand request, CancellationToken cancellationToken)
    {
        var type = _context.UserContentRestrictionsTypes
                    .Where(t => t.Id == request.TypeId)
                    .SingleOrDefault();

        var user = _context.UserContentRestrictions
                    .Where(u => u.UserId == request.UserId)
                    .SingleOrDefault();

        if (user != null)
        {
            return Result.Failure(ContentRestrictionsError.UserAlreadyInList);
        }

        if (type == null)
        {
            return Result.Failure(ContentRestrictionsError.TypeNotFound);
        }

        if( request.ExpiredAt <  DateTime.UtcNow )
        {
            return Result.Failure(ContentRestrictionsError.MustFromFuture);
        }

        _context.UserContentRestrictions.Add( new UserContentRestrictions
        {
            UserId = request.UserId,
            UserContentRestrictionsTypeId = request.TypeId,
            ExpiredAt = request.ExpiredAt
        });

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        await _serviceBus.Publish<CreateNotificationEvent>(new CreateNotificationEvent
        {
            ActionCode = "POST_WARNING",
            CategoryCode = "USER",
            Url = "",
            ActorIds = [request.AdminId],
            OwnerId = request.UserId
        });

        return Result.Success();
    }
}
