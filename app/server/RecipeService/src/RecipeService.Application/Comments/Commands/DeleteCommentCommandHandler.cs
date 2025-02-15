using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
namespace RecipeService.Application.Comments.Commands;
public class DeleteCommentCommand : IRequest<Result<Comment?>>
{
    public Guid? CommentId { get; init; }
    public Guid? AccountId { get; init; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result<Comment?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCommentCommandHandler> _logger;

    public DeleteCommentCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<DeleteCommentCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Comment?>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var commentId = request.CommentId;
            var accountId = request.AccountId;

            if (commentId == null || accountId == null)
            {
                return Result<Comment?>.Failure(CommentError.AddCommentFail);
            }

            var query = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable()
                          .SelectMany(r => r.Comments, (r, c) => new {RecipeId = r.Id, Comment = c}).Where(c => c.Comment.Id == commentId).SingleOrDefault();

            if (query == null || query.Comment == null)
            {
                return Result<Comment?>.Failure(CommentError.NotFound, "Not found comment");
            }

            if(query.Comment.AccountId != accountId)
            {
                return Result<Comment?>.Failure(CommentError.AddCommentFail, "Permission deny. AuthorId of comment not equal current accountId.");
            }

            var recipe = await _context.Recipes.SingleOrDefaultAsync(r => r.Id == query.RecipeId);

            if (recipe == null)
            {
                return Result<Comment?>.Failure(CommentError.AddCommentFail, "Not found recipe to delete comment.");
            }

            var comment = recipe.Comments.SingleOrDefault(c => c.Id == commentId);

            if (comment == null)
            {
                return Result<Comment?>.Failure(CommentError.AddCommentFail, "Not found comment to delete.");
            }
            recipe.Comments.Remove(comment);
            _context.Recipes.Update(recipe);
            await _unitOfWork.SaveChangeAsync();
            return Result<Comment?>.Success(comment);
        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<Comment?>.Failure(CommentError.AddCommentFail);
        }
    }
}
