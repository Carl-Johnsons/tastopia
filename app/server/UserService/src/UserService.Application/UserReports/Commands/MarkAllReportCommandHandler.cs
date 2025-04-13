using Contract.Constants;
using Contract.Event.TrackingEvent;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Errors;
namespace RecipeService.Application.Reports.Commands;
public record MarkAllReportCommand : IRequest<Result>
{
    public Guid AccountId { get; set; }
    public Guid CurrentAccountId { get; set; }
    public bool IsReopened { get; set; }
}
public class MarkAllReportCommandHandler : IRequestHandler<MarkAllReportCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;

    public MarkAllReportCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(MarkAllReportCommand request,
                               CancellationToken cancellationToken)
    {
        if (request.AccountId == Guid.Empty)
        {
            return Result.Failure(UserReportError.NullParameter, "AccountId is null.");
        }
        var reports = await _context.UserReports.Where(rp => rp.ReportedId == request.AccountId && (rp.Status == (request.IsReopened ? ReportStatus.Done : ReportStatus.Pending))).ToListAsync();

        if(reports == null || reports.Count == 0)
        {
            return Result.Failure(UserReportError.NotFound, "Not found user report.");
        }

        if (reports != null && reports.Count != 0)
        {
            foreach(var rp in reports)
            {
                rp.Status = request.IsReopened ? ReportStatus.Pending : ReportStatus.Done;
            }
            _context.UserReports.UpdateRange(reports);
        }
        
        await _unitOfWork.SaveChangeAsync();

        foreach(var rs in reports!)
        {
            await _serviceBus.Publish(new AddActivityLogEvent
            {
                AccountId = request.CurrentAccountId,
                ActivityType = request.IsReopened ? ActivityType.REOPEN : ActivityType.MARK_COMPLETE,
                EntityId = rs.Id,
                EntityType = ActivityEntityType.REPORT_USER
            });

        }
        return Result.Success();
    }
}
