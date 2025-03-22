using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Reports.Commands;

public record DisableCommentCommand : IRequest<Result>
{
    public Guid CommentId { get; set; }
    public Guid RecipeId { get; set; }
}

public class DisableCommentCommandHandler : IRequestHandler<DisableCommentCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DisableCommentCommandHandler(IApplicationDbContext context,
                                            IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DisableCommentCommand request,
                               CancellationToken cancellationToken)
    {
        var recipeCollection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe));

        var recipeFilter = Builders<Recipe>.Filter.Eq(r => r.Id, request.RecipeId);

        var commentProjection = Builders<Recipe>.Projection.Expression(r =>
            r.Comments.FirstOrDefault(c => c.Id == request.CommentId));

        var comment = await recipeCollection
            .Find(recipeFilter)
            .Project(commentProjection)
            .FirstOrDefaultAsync(cancellationToken);

        if (comment == null)
        {
            return Result.Failure(CommentError.NotFound);
        }

        if (!comment.IsActive)
        {
            return Result.Failure(CommentError.AlreadyInactive);
        }

        var filter = Builders<Recipe>.Filter.And(
            Builders<Recipe>.Filter.Eq(r => r.Id, request.RecipeId),
            Builders<Recipe>.Filter.Eq("Comments.Id", request.CommentId)
        );

        var update = Builders<Recipe>.Update.Set("Comments.$.IsActive", false);

        var updateResult = await recipeCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        if (updateResult.ModifiedCount == 0)
        {
            return Result.Failure(CommentError.NotFound);
        }

        return Result.Success();
    }
}
