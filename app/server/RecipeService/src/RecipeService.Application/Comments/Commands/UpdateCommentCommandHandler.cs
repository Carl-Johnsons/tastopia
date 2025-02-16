using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
namespace RecipeService.Application.Comments.Commands;
public class UpdateCommentCommand : IRequest<Result<Comment?>>
{
    public Guid? CommentId { get; init; }
    public Guid? AccountId { get; init; }
    public string? Content { get; init; }
}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Result<Comment?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCommentCommandHandler> _logger;

    public UpdateCommentCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<UpdateCommentCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Comment?>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var commentId = request.CommentId;
            var accountId = request.AccountId;
            var content = request.Content;

            if (commentId == null || accountId == null || string.IsNullOrEmpty(content))
            {
                return Result<Comment?>.Failure(CommentError.UpdateCommentFail);
            }

            var query = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable()
                          .SelectMany(r => r.Comments, (r, c) => new {RecipeId = r.Id, Comment = c}).Where(c => c.Comment.Id == commentId).SingleOrDefault();

            if (query == null || query.Comment == null)
            {
                return Result<Comment?>.Failure(CommentError.NotFound, "Not found comment");
            }

            if(query.Comment.AccountId != accountId)
            {
                return Result<Comment?>.Failure(CommentError.UpdateCommentFail, "Permission deny. AuthorId of comment not equal current accountId.");
            }

            var recipe = await _context.Recipes.SingleOrDefaultAsync(r => r.Id == query.RecipeId);

            if (recipe == null)
            {
                return Result<Comment?>.Failure(CommentError.UpdateCommentFail, "Not found recipe to update comment.");
            }

            var comment = recipe.Comments.SingleOrDefault(c => c.Id == commentId);

            if (comment == null)
            {
                return Result<Comment?>.Failure(CommentError.UpdateCommentFail, "Not found comment to update.");
            }
            comment.Content = content;
            comment.UpdatedAt = DateTime.UtcNow;

            _context.Recipes.Update(recipe);
            await _unitOfWork.SaveChangeAsync();
            return Result<Comment?>.Success(comment);
        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<Comment?>.Failure(CommentError.UpdateCommentFail);
        }
    }
}
