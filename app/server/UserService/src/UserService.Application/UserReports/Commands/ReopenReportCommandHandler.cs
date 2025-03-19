using Contract.Constants;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
namespace RecipeService.Application.Reports.Commands;
public record ReopenReportCommand : IRequest<Result<UserReport?>>
{
    public Guid ReportId { get; set; }
}
public class ReopenReportCommandHandler : IRequestHandler<ReopenReportCommand, Result<UserReport?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public ReopenReportCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<UserReport?>> Handle(ReopenReportCommand request,
                               CancellationToken cancellationToken)
    {
        if(request.ReportId == Guid.Empty)
        {
            return Result<UserReport?>.Failure(UserReportError.NullParameter, "ReportId is null.");
        }
        var report = await _context.UserReports.SingleOrDefaultAsync(rp => rp.Id == request.ReportId);
        if (report == null)
        {
            return Result<UserReport?>.Failure(UserReportError.NotFound, "Not found report");
        }

        if (report.Status == ReportStatus.Pending)
        {
            return Result<UserReport?>.Failure(UserReportError.AlreadyPending);
        }

        report.Status = ReportStatus.Pending;
        _context.UserReports.Update(report);    
        await _unitOfWork.SaveChangeAsync();
        return Result<UserReport?>.Success(report);
    }
}
