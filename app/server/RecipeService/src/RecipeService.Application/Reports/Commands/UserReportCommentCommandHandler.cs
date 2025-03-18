using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
namespace RecipeService.Application.Reports.Commands;
public class UserReportCommentCommand : IRequest<Result<UserReportCommentResponse?>>
{
    public Guid ReporterId { get; set; }
    public Guid CommentId { get; set; }
    public Guid RecipeId { get; set; }
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
        var reporterId = request.ReporterId;
        var commentId = request.CommentId;
        var recipeId = request.RecipeId;
        var reasonCodes = request.ReasonCodes;
        var additionalDetails = request.AdditionalDetails;

        if (reporterId == Guid.Empty || commentId == Guid.Empty || reasonCodes == null || reasonCodes.Count == 0)
        {
            return Result<UserReportCommentResponse?>.Failure(UserReportCommentError.NullParameter);
        }

        var recipeCollection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe));


        var filter = Builders<Recipe>.Filter.And(
                Builders<Recipe>.Filter.Eq(r => r.Id, recipeId),
                Builders<Recipe>.Filter.ElemMatch(r => r.Comments, c => c.Id == commentId)
            );

        var projection = Builders<Recipe>.Projection.Expression(r => r.Comments.FirstOrDefault(c => c.Id == commentId));

        var comment = recipeCollection.Find(filter).Project(projection).SingleOrDefault();

        if (comment == null)
        {
            return Result<UserReportCommentResponse?>.Failure(UserReportCommentError.NotFound, "Comment not found");
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
            RecipeId = recipeId,
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
}
