using Contract.Constants;
using Contract.Event.TrackingEvent;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Errors;
using UserService.Domain.Responses;
namespace RecipeService.Application.Reports.Commands;
public record MarkReportCommand : IRequest<Result<AdminMarkReportResponse?>>
{
    public Guid ReportId { get; set; }
    public Guid CurrentAccountId { get; set; }
}
public class MarkReportCommandHandler : IRequestHandler<MarkReportCommand, Result<AdminMarkReportResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    public MarkReportCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork,
                                            IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }
    public async Task<Result<AdminMarkReportResponse?>> Handle(MarkReportCommand request,
                               CancellationToken cancellationToken)
    {
        if (request.ReportId == Guid.Empty)
        {
            return Result<AdminMarkReportResponse?>.Failure(UserReportError.NullParameter, "ReportId is null.");
        }
        var report = await _context.UserReports.SingleOrDefaultAsync(rp => rp.Id == request.ReportId);
        if (report == null)
        {
            return Result<AdminMarkReportResponse?>.Failure(UserReportError.NotFound, "Not found report");
        }
        ActivityType activityType;

        switch (report.Status)
        {
            case ReportStatus.Pending:
                report.Status = ReportStatus.Done;
                activityType = ActivityType.MARK_COMPLETE;
                break;
            default:
                report.Status = ReportStatus.Pending;
                activityType = ActivityType.RESTORE;
                break;
        }
        _context.UserReports.Update(report);
        var result = new AdminMarkReportResponse
        {
            UserReport = report,
            IsReopened = report.Status == ReportStatus.Pending,
        };
        await _unitOfWork.SaveChangeAsync();

        await _serviceBus.Publish(new AddActivityLogEvent
        {
            AccountId = request.CurrentAccountId,
            ActivityType = activityType,
            EntityId = request.ReportId,
            EntityType = ActivityEntityType.REPORT_USER
        });

        return Result<AdminMarkReportResponse?>.Success(result);
    }
}
