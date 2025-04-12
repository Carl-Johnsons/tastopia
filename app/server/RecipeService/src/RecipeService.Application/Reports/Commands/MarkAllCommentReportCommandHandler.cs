using Contract.Constants;
using Contract.Event.TrackingEvent;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Errors;
namespace RecipeService.Application.Reports.Commands;

public record MarkAllCommentReportCommand : IRequest<Result>
{
    public Guid RecipeId { get; set; }
    public Guid CommentId { get; set; }
    public Guid CurrentAccountId { get; set; }
    public bool IsReopened { get; set; }
}

public class MarkAllCommentReportCommandHandler : IRequestHandler<MarkAllCommentReportCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    public MarkAllCommentReportCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(MarkAllCommentReportCommand request,
                               CancellationToken cancellationToken)
    {
        if(request.RecipeId == Guid.Empty || request.CommentId == Guid.Empty)
        {
            return Result.Failure(ReportError.NullParameter, "RecipeId or CommentId not found");
        }

        var reports = await _context.UserReportComments.Where(rp => (rp.RecipeId == request.RecipeId || rp.EntityId == request.CommentId) && rp.Status == (request.IsReopened ? ReportStatus.Done : ReportStatus.Pending)).ToListAsync();
        if(reports == null || reports.Count == 0)
        {
            return Result.Failure(ReportError.NotFound, "Not found comment reports");
        }
        if (reports != null && reports.Count != 0)
        {
            foreach (var rp in reports)
            {
                rp.Status = request.IsReopened ? ReportStatus.Pending : ReportStatus.Done;
            }
            _context.UserReportComments.UpdateRange(reports);
        }
        await _unitOfWork.SaveChangeAsync();
        foreach (var rs in reports!)
        {
            await _serviceBus.Publish(new AddActivityLogEvent
            {
                AccountId = request.CurrentAccountId,
                ActivityType = request.IsReopened ? ActivityType.REOPEN : ActivityType.MARK_COMPLETE,
                EntityId = rs.Id,
                EntityType = ActivityEntityType.REPORT_COMMENT
            });

        }
        return Result.Success();
    }

}
