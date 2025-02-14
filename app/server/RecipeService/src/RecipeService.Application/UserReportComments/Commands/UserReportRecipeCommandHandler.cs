using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
namespace RecipeService.Application.UserReportComments.Commands;
public class UserReportCommentCommand : IRequest<Result<UserReportComment?>>
{
    public Guid ReporterId { get; set; }
    public Guid CommentId { get; set; }
    public string Reason { get; set; } = null!;
}

public class UserReportCommentCommandHandler : IRequestHandler<UserReportCommentCommand, Result<UserReportComment?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserReportCommentCommandHandler> _logger;

    public UserReportCommentCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<UserReportCommentCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<UserReportComment?>> Handle(UserReportCommentCommand request, CancellationToken cancellationToken)
    {
        try {
            var reporterId = request.ReporterId;
            var commentId = request.CommentId;
            var reason = request.Reason;

            if (reporterId == Guid.Empty || commentId == Guid.Empty || string.IsNullOrEmpty(reason))
            {
                return Result<UserReportComment?>.Failure(UserReportCommentError.NullParameter);
            }

            var report = _context.UserReportComments.Where(r => r.AccountId == reporterId && r.CommentId == commentId).FirstOrDefault();

            if (report != null)
            {
                return Result<UserReportComment?>.Failure(UserReportCommentError.AddUserReportCommentFail, "This report is adready added before.");
            }

            report = new UserReportComment
            {
                AccountId = reporterId,
                CommentId = commentId,
                Reason = reason,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _context.UserReportComments.Add(report);
            await _unitOfWork.SaveChangeAsync();
            return Result<UserReportComment?>.Success(report);
        }
        catch (Exception ex) {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<UserReportComment?>.Failure(UserReportCommentError.AddUserReportCommentFail, ex.Message);
        }
    }
}
