using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
namespace RecipeService.Application.UserReportComments.Commands;
public class UserReportCommentCommand : IRequest<Result<UserReportCommentResponse?>>
{
    public Guid ReporterId { get; set; }
    public Guid CommentId { get; set; }
    public string Reason { get; set; } = null!;
}

public class UserReportCommentCommandHandler : IRequestHandler<UserReportCommentCommand, Result<UserReportCommentResponse?>>
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

    public async Task<Result<UserReportCommentResponse?>> Handle(UserReportCommentCommand request, CancellationToken cancellationToken)
    {
        try {
            var reporterId = request.ReporterId;
            var commentId = request.CommentId;
            var reason = request.Reason;

            if (reporterId == Guid.Empty || commentId == Guid.Empty || string.IsNullOrEmpty(reason))
            {
                return Result<UserReportCommentResponse?>.Failure(UserReportCommentError.NullParameter);
            }

            var report = _context.UserReportComments.Where(r => r.AccountId == reporterId && r.CommentId == commentId).FirstOrDefault();
            if (report != null)
            {
                _context.UserReportComments.Remove(report);
                await _unitOfWork.SaveChangeAsync();
                return Result<UserReportCommentResponse?>.Success(new UserReportCommentResponse
                {
                    Report = report,
                    IsRemoved = true,
                });
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
            return Result<UserReportCommentResponse?>.Success(new UserReportCommentResponse
            {
                Report = report,
                IsRemoved = false
            });
        }
        catch (Exception ex) {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<UserReportCommentResponse?>.Failure(UserReportCommentError.AddUserReportCommentFail, ex.Message);
        }
    }
}
