using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UserService.Domain.Entities;
using UserService.Domain.Errors;
using UserService.Domain.Responses;
namespace UserService.Application.UserReports.Commands;
public class UserReportUserCommand : IRequest<Result<UserReportUserResponse?>>
{
    public Guid ReporterId { get; set; }
    public Guid ReportedId { get; set; }
    public List<string> ReasonCodes { get; set; } = null!;
    public string? AdditionalDetails { get; set; }
}

public class UserReportUserCommandHandler : IRequestHandler<UserReportUserCommand, Result<UserReportUserResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserReportUserCommandHandler> _logger;

    public UserReportUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<UserReportUserCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<UserReportUserResponse?>> Handle(UserReportUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var reporterId = request.ReporterId;
            var reportedId = request.ReportedId;
            var reasonCodes = request.ReasonCodes;
            var AdditionalDetails = request.AdditionalDetails;

            if (reporterId == Guid.Empty || reportedId == Guid.Empty || reasonCodes == null || reasonCodes.Count == 0)
            {
                return Result<UserReportUserResponse?>.Failure(UserReportError.NullParameter);
            }

            if (reporterId == reportedId)
            {
                return Result<UserReportUserResponse?>.Failure(UserReportError.AddUserReportFail, "Reporter id is equal reported id");
            }

            var users = await _context.Users
                .Where(u => u.AccountId == reporterId || u.AccountId == reportedId)
                .ToDictionaryAsync(u => u.AccountId);

            var reporter = users.GetValueOrDefault(reporterId);
            var reported = users.GetValueOrDefault(reportedId);

            if(reporter == null || reported == null) {
                return Result<UserReportUserResponse?>.Failure(UserReportError.AddUserReportFail, "Not found reporter or reported");
            }

            var report = _context.UserReports.Where(r => r.ReporterId == reporterId && r.ReportedId == reportedId).FirstOrDefault();
            if (report != null)
            {
                _context.UserReports.Remove(report);
                await _unitOfWork.SaveChangeAsync();
                return Result<UserReportUserResponse?>.Success(new UserReportUserResponse
                {
                    Report = report,
                    IsRemoved = true,
                });
            }

            var codes = ReportReasonData.ReportUserReasons.Where(r => reasonCodes.Contains(r.Code)).Select(r => r.Code).ToList();

            report = new UserReport
            {
                ReporterId = reporterId,
                ReportedId = reportedId,
                ReasonCodes = codes,
                AdditionalDetails = AdditionalDetails,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _context.UserReports.Add(report);
            await _unitOfWork.SaveChangeAsync();
            return Result<UserReportUserResponse?>.Success(new UserReportUserResponse
            {
                Report = report,
                IsRemoved = false
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<UserReportUserResponse?>.Failure(UserReportError.AddUserReportFail, ex.Message);
        }
    }
}
