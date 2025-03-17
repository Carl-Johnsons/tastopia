using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
namespace RecipeService.Application.Reports.Commands;
public class UserReportCommentCommand : IRequest<Result<UserReportCommentResponse?>>
{
    public Guid ReporterId { get; set; }
    public Guid CommentId { get; set; }
    public List<string> ReasonCodes { get; set; } = null!;
    public string? AdditionalDetails { get; set; }
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
        try
        {
            var reporterId = request.ReporterId;
            var commentId = request.CommentId;
            var reasonCodes = request.ReasonCodes;
            var additionalDetails = request.AdditionalDetails;

            if (reporterId == Guid.Empty || commentId == Guid.Empty || reasonCodes == null || reasonCodes.Count == 0)
            {
                return Result<UserReportCommentResponse?>.Failure(UserReportCommentError.NullParameter);
            }

            var comment = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().SelectMany(r => r.Comments).Where(c => c.Id == commentId).SingleOrDefault();

            if (comment == null)
            {
                return Result<UserReportCommentResponse?>.Failure(UserReportCommentError.NotFound, "Not found comment");
            }

            var report = _context.UserReportComments.Where(r => r.AccountId == reporterId && r.EntityId == commentId).FirstOrDefault();
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

            var codes = ReportReasonData.CommentReportReasons.Where(r => reasonCodes.Contains(r.Code)).Select(r => r.Code).ToList();
            if (codes == null || codes.Count == 0)
            {
                return Result<UserReportCommentResponse?>.Failure(UserReportCommentError.NotFound, "Not found reason codes.");
            }

            report = new UserReportComment
            {
                AccountId = reporterId,
                EntityId = commentId,
                ReasonCodes = codes,
                AdditionalDetails = additionalDetails,
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
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<UserReportCommentResponse?>.Failure(UserReportCommentError.AddUserReportCommentFail, ex.Message);
        }
    }
}
