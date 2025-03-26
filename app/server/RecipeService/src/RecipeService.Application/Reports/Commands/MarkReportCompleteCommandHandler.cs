
using Contract.Constants;
using Contract.Event.TrackingEvent;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Reports.Commands;

public record MarkReportCompleteCommand : IRequest<Result>
{
    public Guid ReportId { get; set; }
    public ReportType ReportType { get; set; }
    public Guid CurrentAccountId { get; set; }
}

public class MarkReportCompleteCommandHandler : IRequestHandler<MarkReportCompleteCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    public MarkReportCompleteCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork,
                                            IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result> Handle(MarkReportCompleteCommand request,
                               CancellationToken cancellationToken)
    {
        var report = await GetReportEntityAsync(request.ReportType, request.ReportId, cancellationToken);

        if (report == null)
        {
            return Result.Failure(ReportError.NotFound);
        }

        if (report.Status == ReportStatus.Done)
        {
            return Result.Failure(ReportError.AlreadyMarkComplete);
        }

        report.Status = ReportStatus.Done;

        UpdateReportEntity(request.ReportType, report);

        await _unitOfWork.SaveChangeAsync();

        switch (request.ReportType)
        {
            case ReportType.RECIPE:
                _context.UserReportRecipes.Update((UserReportRecipe)report);
                await _serviceBus.Publish(new AddActivityLogEvent
                {
                    AccountId = request.CurrentAccountId,
                    ActivityType = ActivityType.MARK_COMPLETE,
                    EntityId = report.Id,
                    EntityType = ActivityEntityType.REPORT_RECIPE
                });
                break;
            case ReportType.COMMENT:
                _context.UserReportComments.Update((UserReportComment)report);
                await _serviceBus.Publish(new AddActivityLogEvent
                {
                    AccountId = request.CurrentAccountId,
                    ActivityType = ActivityType.MARK_COMPLETE,
                    EntityId = report.Id,
                    EntityType = ActivityEntityType.REPORT_COMMENT
                });
                break;
            default:
                throw new NotImplementedException();
        }

        return Result.Success();
    }

    private async Task<CommonReportEntity?> GetReportEntityAsync(ReportType reportType, Guid reportId, CancellationToken cancellationToken)
    {
        return reportType switch
        {
            ReportType.RECIPE => await _context.UserReportRecipes.SingleOrDefaultAsync(x => x.Id == reportId, cancellationToken),
            ReportType.COMMENT => await _context.UserReportComments.SingleOrDefaultAsync(x => x.Id == reportId, cancellationToken),
            _ => throw new NotImplementedException()
        };
    }

    private void UpdateReportEntity(ReportType reportType, CommonReportEntity report)
    {
        switch (reportType)
        {
            case ReportType.RECIPE:
                _context.UserReportRecipes.Update((UserReportRecipe)report);
                break;
            case ReportType.COMMENT:
                _context.UserReportComments.Update((UserReportComment)report);
                break;
            default:
                throw new NotImplementedException();
        }
    }

}
