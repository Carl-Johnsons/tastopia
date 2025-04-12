using Contract.Constants;
using Contract.Event.TrackingEvent;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Errors;
using UserService.Domain.Responses;
namespace RecipeService.Application.Reports.Commands;
public record MarkAllReportCommand : IRequest<Result<List<AdminMarkReportResponse>?>>
{
    public Guid AccountId { get; set; }
    public Guid CurrentAccountId { get; set; }
    public bool IsReopened { get; set; }
}
public class MarkAllReportCommandHandler : IRequestHandler<MarkAllReportCommand, Result<List<AdminMarkReportResponse>?>>
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

    public async Task<Result<List<AdminMarkReportResponse>?>> Handle(MarkAllReportCommand request,
                               CancellationToken cancellationToken)
    {
        if (request.AccountId == Guid.Empty)
        {
            return Result<List<AdminMarkReportResponse>?>.Failure(UserReportError.NullParameter, "AccountId is null.");
        }
        var reports = await _context.UserReports.Where(rp => rp.ReportedId == request.AccountId).ToListAsync();

        var result = new List<AdminMarkReportResponse>();
        if (reports != null && reports.Count != 0)
        {
            foreach(var rp in reports)
            {
                rp.Status = request.IsReopened ? ReportStatus.Pending : ReportStatus.Done;
                result.Add(new AdminMarkReportResponse
                {
                    UserReport = rp,
                    IsReopened = request.IsReopened
                });
            }
            _context.UserReports.UpdateRange(reports);
        }
        
        await _unitOfWork.SaveChangeAsync();

        foreach(var rs in result)
        {
            await _serviceBus.Publish(new AddActivityLogEvent
            {
                AccountId = request.CurrentAccountId,
                ActivityType = request.IsReopened ? ActivityType.REOPEN : ActivityType.MARK_COMPLETE,
                EntityId = rs.UserReport.Id,
                EntityType = ActivityEntityType.REPORT_USER
            });

        }
        return Result<List<AdminMarkReportResponse>?>.Success(result);
    }
}
