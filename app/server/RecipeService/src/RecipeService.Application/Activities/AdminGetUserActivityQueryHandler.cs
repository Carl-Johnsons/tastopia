using AutoMapper;
using Contract.Constants;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UserProto;
namespace RecipeService.Application.Activities;
public class AdminGetUserActivityQuery : IRequest<Result<PaginatedUserActivityListResponse?>>
{
    public int? Skip { get; set; }
    public string? Language { get; set; }
    public Guid AccountId { get; set; }
}
public class AdminGetUserActivityQueryHandler : IRequestHandler<AdminGetUserActivityQuery, Result<PaginatedUserActivityListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public AdminGetUserActivityQueryHandler(IApplicationDbContext context, IMapper mapper, GrpcUser.GrpcUserClient grpcUserClient)
    {
        _context = context;
        _mapper = mapper;
        _grpcUserClient = grpcUserClient;
    }

    public async Task<Result<PaginatedUserActivityListResponse?>> Handle(AdminGetUserActivityQuery request, CancellationToken cancellationToken)
    {
        var skip = request.Skip;
        var accountId = request.AccountId;
        var lang = request.Language;
        if (accountId == Guid.Empty || skip == null || string.IsNullOrEmpty(lang))
        {
            return Result<PaginatedUserActivityListResponse?>.Failure(RecipeError.NullParameter, "AccountId, Language or Skip is null");
        }

        var userResponse = await _grpcUserClient.GetUserDetailAsync(new GrpcAccountIdRequest
        {
            AccountId = accountId.ToString(),
        }, cancellationToken: cancellationToken);

        if (userResponse == null || userResponse.IsAdmin)
        {
            return Result<PaginatedUserActivityListResponse?>.Failure(RecipeError.PermissionDeny, "Cannot view other admin profile.");
        }
        var recipesQuery = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().Where(r => r.AuthorId == accountId && r.IsActive).OrderByDescending(r => r.CreatedAt).Select(
            r => new UserActivityResponse
            {
                AccountId = accountId,
                AvtImageUrl = "",
                Username = "",
                Title = "",
                TimeAgo = "",
                Type = UserActivityType.CreateRecipe,
                Description = "",
                RecipeId = r.Id,
                RecipeTitle = r.Title,
                RecipeAuthorId = r.AuthorId,
                RecipeAuthorUsername = "",
                RecipeImageUrl = r.ImageUrl,
                RecipeTimeAgo = "",
                Time = r.CreatedAt,
                RecipeTime = r.CreatedAt,
                RecipeVoteDiff = r.VoteDiff,
            }
            ).AsQueryable();

        var commentQuery = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().SelectMany(r => r.Comments, (r, c) => new { Recipe = r, Comment = c }).Where(rc => rc.Comment.AccountId == accountId).OrderByDescending(rc => rc.Comment.CreatedAt).Select(
            rc => new UserActivityResponse
            {
                AccountId = accountId,
                AvtImageUrl = "",
                Username = "",
                Title = "",
                TimeAgo = "",
                Type = UserActivityType.CommentRecipe,
                Description = "",
                RecipeId = rc.Recipe.Id,
                RecipeTitle = rc.Recipe.Title,
                RecipeAuthorId = rc.Recipe.AuthorId,
                RecipeAuthorUsername = "",
                RecipeImageUrl = rc.Recipe.ImageUrl,
                RecipeTimeAgo = "",
                Time = rc.Comment.CreatedAt,
                RecipeTime = rc.Recipe.CreatedAt,
                RecipeVoteDiff = rc.Recipe.VoteDiff,
                CommentId = rc.Comment.Id,
                CommentContent = rc.Comment.Content,
            }
            ).AsQueryable();
        var voteQuery = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().SelectMany(r => r.RecipeVotes, (r, v) => new { Recipe = r, Vote = v }).Where(rv => rv.Vote.AccountId == accountId).OrderByDescending(rv => rv.Vote.CreatedAt).Select(
            rv => new UserActivityResponse
            {
                AccountId = accountId,
                AvtImageUrl = "",
                Username = "",
                Title = "",
                TimeAgo = "",
                Type = rv.Vote.IsUpvote ? UserActivityType.UpvoteRecipe : UserActivityType.DownvoteRecipe,
                Description = "",
                RecipeId = rv.Recipe.Id,
                RecipeTitle = rv.Recipe.Title,
                RecipeAuthorId = rv.Recipe.AuthorId,
                RecipeAuthorUsername = "",
                RecipeImageUrl = rv.Recipe.ImageUrl,
                RecipeTimeAgo = "",
                Time = rv.Vote.CreatedAt,
                RecipeTime = rv.Recipe.CreatedAt,
                RecipeVoteDiff = rv.Recipe.VoteDiff,
            }
            ).AsQueryable();

        var combinedQuery = recipesQuery
        .Union(commentQuery)
        .Union(voteQuery)
        .OrderByDescending(a => a.Time).AsQueryable();


        var authorIds = combinedQuery
        .Select(a => a.RecipeAuthorId)
        .Distinct()
        .ToHashSet();
        authorIds.Add(accountId);

        var totalPage = (combinedQuery.Count() + ActivityConstant.ACTIVITY_LIMIT - 1) / ActivityConstant.ACTIVITY_LIMIT;
        combinedQuery = combinedQuery.Skip(ActivityConstant.ACTIVITY_LIMIT * skip.Value).Take(ActivityConstant.ACTIVITY_LIMIT);


        var usersResponse = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { _mapper.Map<RepeatedField<string>>(authorIds) }
        }, cancellationToken: cancellationToken);

        var mapUsers = new Dictionary<Guid, SimpleUser>();
        foreach (var (key, value) in usersResponse.Users)
        {
            mapUsers[Guid.Parse(key)] = new SimpleUser
            {
                AccountId = Guid.Parse(value.AccountId),
                AvtUrl = value.AvtUrl,
                DisplayName = value.DisplayName,
                AccountUsername = value.AccountUsername,
            };
        }
        if (usersResponse == null || mapUsers.Count != authorIds.Count)
        {
            return Result<PaginatedUserActivityListResponse?>.Failure(RecipeError.NotFound);
        }
        var activities = combinedQuery.ToList();
        foreach (var a in activities)
        {
            a.AvtImageUrl = mapUsers[accountId].AvtUrl;
            a.Username = mapUsers[accountId].AccountUsername;
            if (a.Type == UserActivityType.CreateRecipe)
            {
                a.Title = ActionTemplateConstant.Data.SingleOrDefault(t => t.TemplateCode == ActionTemplateCode.USER_CREATE_RECIPE)!.TranslationMessages[lang];
                a.TimeAgo = ActionTemplateConstant.GetTimeElapsed(a.Time, lang);
                a.Description = FormatString(ActionTemplateConstant.Data.SingleOrDefault(t => t.TemplateCode == ActionTemplateCode.USER_CREATE_RECIPE)!.TranslationMessages[lang], a.RecipeTitle!);
                a.RecipeTimeAgo = ActionTemplateConstant.GetTimeElapsed(a.RecipeTime!.Value, lang);
            }
            if (a.Type == UserActivityType.CommentRecipe)
            {
                a.Title = ActionTemplateConstant.Data.SingleOrDefault(t => t.TemplateCode == ActionTemplateCode.USER_COMMENT)!.TranslationMessages[lang];
                a.TimeAgo = ActionTemplateConstant.GetTimeElapsed(a.Time, lang);
                a.Description = FormatString(ActionTemplateConstant.Data.SingleOrDefault(t => t.TemplateCode == ActionTemplateCode.USER_COMMENT)!.TranslationMessages[lang], a.CommentContent!);
                a.RecipeTimeAgo = ActionTemplateConstant.GetTimeElapsed(a.RecipeTime!.Value, lang);
            }
            if (a.Type == UserActivityType.UpvoteRecipe || a.Type == UserActivityType.DownvoteRecipe)
            {
                a.Title = ActionTemplateConstant.Data.SingleOrDefault(t => t.TemplateCode == (a.Type == UserActivityType.UpvoteRecipe ? ActionTemplateCode.USER_UPVOTE : ActionTemplateCode.USER_DOWNVOTE))!.TranslationMessages[lang];
                a.TimeAgo = ActionTemplateConstant.GetTimeElapsed(a.Time, lang);
                a.Description = ActionTemplateConstant.Data.SingleOrDefault(t => t.TemplateCode == (a.Type == UserActivityType.UpvoteRecipe ? ActionTemplateCode.USER_UPVOTE : ActionTemplateCode.USER_DOWNVOTE))!.TranslationMessages[lang].TrimEnd('.');
                a.RecipeTimeAgo = ActionTemplateConstant.GetTimeElapsed(a.RecipeTime!.Value, lang);
            }
            if (a.RecipeAuthorUsername != null)
            {
                a.RecipeAuthorUsername = mapUsers[a.RecipeAuthorId!.Value].AccountUsername;
            }
        }

        var hasNextPage = true;

        if (skip >= totalPage - 1)
        {
            hasNextPage = false;
        }
        var paginatedResponse = new PaginatedUserActivityListResponse
        {
            PaginatedData = activities,
            Metadata = new AdvancePaginatedMetadata
            {
                TotalPage = totalPage,
                HasNextPage = hasNextPage
            }
        };
        return Result<PaginatedUserActivityListResponse?>.Success(paginatedResponse);
    }

    string FormatString(string str1, string str2)
    {
        int maxLength = 35;
        return str1.TrimEnd('.') + " \"" + (str2.Length > maxLength ? str2.Substring(0, maxLength) + "..." : str2) + "\"";
    }

}
