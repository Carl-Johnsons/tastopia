
using Contract.Constants;

namespace TrackingService.Application.ActivityLog.Commands;

public class AddActivityLogCommand : IRequest<Result>
{
    public Guid AccountId { get; set; }

    public ActivityType ActivityType { get; set; }
    public Guid EntityId { get; set; }
    public ActivityEntityType EntityType { get; set; }
    public Guid? SecondaryEntityId { get; set; }
    public ActivityEntityType? SecondaryEntityType { get; set; }
}

public class AddActivityLogCommandHandler : IRequestHandler<AddActivityLogCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public AddActivityLogCommandHandler(IApplicationDbContext context,
                                        IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddActivityLogCommand request,
                               CancellationToken cancellationToken)
    {

        _context.AdminActivityLogs.Add(new Domain.Entities.AdminActivityLog
        {
            AccountId = request.AccountId,
            ActivityType = request.ActivityType,
            EntityId = request.EntityId,
            EntityType = request.EntityType,
            SecondaryEntityId = request.SecondaryEntityId,
            SecondaryEntityType = request.SecondaryEntityType
        });

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Success();
    }
}
