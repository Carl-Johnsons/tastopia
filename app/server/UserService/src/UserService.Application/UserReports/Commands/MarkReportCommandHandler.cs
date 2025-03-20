using Contract.Constants;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Errors;
using UserService.Domain.Responses;
namespace RecipeService.Application.Reports.Commands;
public record MarkReportCommand : IRequest<Result<AdminMarkReportResponse?>>
{
    public Guid ReportId { get; set; }
    public Guid AccountId { get; set; }

}
public class MarkReportCommandHandler : IRequestHandler<MarkReportCommand, Result<AdminMarkReportResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public MarkReportCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<AdminMarkReportResponse?>> Handle(MarkReportCommand request,
                               CancellationToken cancellationToken)
    {
        if(request.ReportId == Guid.Empty)
        {
            return Result<AdminMarkReportResponse?>.Failure(UserReportError.NullParameter, "ReportId is null.");
        }

        if (request.AccountId == Guid.Empty)
        {
            return Result<AdminMarkReportResponse>.Failure(UserReportError.NullParameter, "AccountId, CurrentAccountId or Skip is null");
        }

        var users = await _context.Users
            .SingleOrDefaultAsync(u => u.AccountId == request.AccountId);

        if (users == null)
        {
            return Result<AdminMarkReportResponse?>.Failure(UserError.NotFound, "Not found current user.");
        }
        if (!users.IsAdmin)
        {
            return Result<AdminMarkReportResponse?>.Failure(UserError.PermissionDenied);
        }

        var report = await _context.UserReports.SingleOrDefaultAsync(rp => rp.Id == request.ReportId);
        if (report == null)
        {
            return Result<AdminMarkReportResponse?>.Failure(UserReportError.NotFound, "Not found report");
        }
        switch (report.Status) {
            case ReportStatus.Pending:
                report.Status = ReportStatus.Done;
                break;
            case ReportStatus.Done:
                report.Status = ReportStatus.Pending;
                break;
        }
        _context.UserReports.Update(report);
        var result = new AdminMarkReportResponse
        {
            UserReport = report,
            IsReopened = report.Status == ReportStatus.Pending,
        };
        await _unitOfWork.SaveChangeAsync();
        return Result<AdminMarkReportResponse?>.Success(result);
    }
}
