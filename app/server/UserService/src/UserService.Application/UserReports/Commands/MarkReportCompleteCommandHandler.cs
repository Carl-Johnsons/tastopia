using Contract.Constants;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
namespace RecipeService.Application.Reports.Commands;
public record MarkReportCompleteCommand : IRequest<Result<UserReport?>>
{
    public Guid ReportId { get; set; }
}
public class MarkReportCompleteCommandHandler : IRequestHandler<MarkReportCompleteCommand, Result<UserReport?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public MarkReportCompleteCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<UserReport?>> Handle(MarkReportCompleteCommand request,
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
        if (report.Status == ReportStatus.Done)
        {
            return Result<UserReport?>.Failure(UserReportError.AlreadyMarkComplete);
        }
        report.Status = ReportStatus.Done;
        _context.UserReports.Update(report);    
        await _unitOfWork.SaveChangeAsync();
        return Result<UserReport?>.Success(report);
    }
}
