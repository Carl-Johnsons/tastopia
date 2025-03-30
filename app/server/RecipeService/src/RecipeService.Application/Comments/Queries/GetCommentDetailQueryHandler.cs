
using AutoMapper;
using Google.Protobuf.Collections;
using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.Reports.Queries;
public record GetCommentDetailQuery : IRequest<Result<Dictionary<string, CommentDetailResponse>?>>
{
    // Key syntax is "recipeId~commentId"
    public HashSet<string> RecipeAndCommentIdSet { get; init; } = null!;
}


public class GetCommentDetailQueryHandler : IRequestHandler<GetCommentDetailQuery, Result<Dictionary<string, CommentDetailResponse>?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public GetCommentDetailQueryHandler(IApplicationDbContext context,
                                              GrpcUser.GrpcUserClient grpcUserClient,
                                              IMapper mapper)
    {
        _context = context;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
    }

    public async Task<Result<Dictionary<string, CommentDetailResponse>?>> Handle(GetCommentDetailQuery request, CancellationToken cancellationToken)
    {
        var userReportCommentCollection = _context.GetDatabase().GetCollection<UserReportComment>(nameof(UserReportComment));
        var recipeCollection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe));

        var filters = new List<FilterDefinition<Recipe>>();

        foreach (var compositeKey in request.RecipeAndCommentIdSet)
        {
            var splitArray = compositeKey.Split('~');
            var recipeId = Guid.Parse(splitArray[0]);
            var commentId = Guid.Parse(splitArray[1]);


            var filter = Builders<Recipe>.Filter.And(
                Builders<Recipe>.Filter.Eq(r => r.Id, recipeId),
                Builders<Recipe>.Filter.ElemMatch(r => r.Comments, c => c.Id == commentId)
            );
            filters.Add(filter);
        }

        var combinedFilter = Builders<Recipe>.Filter.Or(filters);
        var pipeline = recipeCollection.Aggregate()
            .Match(combinedFilter)
            .Project(r => new Recipe
            {
                Id = r.Id,
                Comments = r.Comments
                    .Where(c => request.RecipeAndCommentIdSet.Contains(r.Id.ToString() + "~" + c.Id.ToString()))
                    .ToList()
            })
            .ToListAsync(cancellationToken: cancellationToken);

        var recipes = await pipeline;

        var flattenCommentQuery = recipes
            .SelectMany(r => r.Comments.Select(c => new CommentWithRecipeId
            {
                CompositeId = r.Id.ToString() + "~" + c.Id.ToString(),
                Comment = c
            }));

        if (flattenCommentQuery.Count() == 0)
        {
            return Result<Dictionary<string, CommentDetailResponse>?>.Failure(CommentError.NotFound);
        }
        var accountIds = flattenCommentQuery.Select(c => c.Comment.AccountId).ToList();
        var repeatedField = _mapper.Map<RepeatedField<string>>(accountIds);

        var grpcUserMap = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { repeatedField }
        }, cancellationToken: cancellationToken);

        var commentsDictionary = flattenCommentQuery.ToDictionary(c => c.CompositeId, c => new CommentDetailResponse
        {
            Id = c.Comment.Id,
            Content = c.Comment.Content,
            AuthorId = c.Comment.AccountId,
            CreatedAt = c.Comment.CreatedAt,
            UpdatedAt = c.Comment.UpdatedAt,
            IsActive = c.Comment.IsActive,
            AuthorAvatarURL = grpcUserMap.Users[c.Comment.AccountId.ToString()].AvtUrl,
            AuthorDisplayName = grpcUserMap.Users[c.Comment.AccountId.ToString()].DisplayName,
            AuthorUsername = grpcUserMap.Users[c.Comment.AccountId.ToString()].AccountUsername,
        });

        return Result<Dictionary<string, CommentDetailResponse>?>.Success(commentsDictionary);
    }

    private class CommentWithRecipeId
    {
        public string CompositeId { get; set; } = null!;
        public Comment Comment { get; set; } = null!;
    }
}
