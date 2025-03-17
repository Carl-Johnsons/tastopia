
using Contract.Constants;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Reports.Commands;

public record ReopenReportCommand : IRequest<Result>
{
    public Guid ReportId { get; set; }
    public ReportType ReportType { get; set; }
}

public class ReopenReportCommandHandler : IRequestHandler<ReopenReportCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public ReopenReportCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ReopenReportCommand request,
                               CancellationToken cancellationToken)
    {
        var report = await GetReportEntityAsync(request.ReportType, request.ReportId, cancellationToken);

        if (report == null)
        {
            return Result.Failure(ReportError.NotFound);
        }

        if (report.Status == ReportStatus.Pending)
        {
            return Result.Failure(ReportError.AlreadyPending);
        }

        report.Status = ReportStatus.Pending;

        UpdateReportEntity(request.ReportType, report);

        await _unitOfWork.SaveChangeAsync();

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
