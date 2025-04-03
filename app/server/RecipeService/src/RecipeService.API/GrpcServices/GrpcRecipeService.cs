using AutoMapper;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RecipeProto;
using RecipeService.Application.Recipes.Queries;
using RecipeService.Application.Reports.Queries;
using RecipeService.Application.Tags.Queries;

namespace RecipeService.API.GrpcServices;

public class GrpcRecipeService : GrpcRecipe.GrpcRecipeBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ILogger<GrpcRecipeService> _logger;

    public GrpcRecipeService(ISender sender, IMapper mapper, ILogger<GrpcRecipeService> logger)
    {
        _sender = sender;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<GrpcListTagDTO> GetAllTags(GrpcEmpty request, ServerCallContext context)
    {
        var response = await _sender.Send(new GetAllTagsQuery());
        response.ThrowIfFailure();
        var result = new GrpcListTagDTO();
        foreach (var t in response.Value)
        {
            result.Tags.Add(new GrpcTagDTO
            {
                Id = t.Id.ToString(),
                Code = t.Code,
                Category = t.Category.ToString(),
                En = t.Value.En ?? "",
                Vi = t.Value.Vi ?? "",
                ImageUrl = t.ImageUrl,
                Status = t.Status.ToString()
            });
        }
        _logger.LogInformation("Grpc GetAllTags successfully!");
        return result;
    }

    public override async Task<GrpcRecipeDetailsDTO> GetRecipeDetails(GrpcRecipeIdRequest request, ServerCallContext context)
    {
        var response = await _sender.Send(new GetRecipeDetailForServerQuery
        {
            RecipeId = Guid.Parse(request.RecipeId)
        });
        response.ThrowIfFailure();

        var result = _mapper.Map<GrpcRecipeDetailsDTO>(response.Value!.Recipe);

        _logger.LogInformation("Grpc GetRecipeDetails successfully!");
        return result;
    }

    public override async Task<GrpcMapSimpleRecipes> GetSimpleRecipes(GrpcGetSimpleRecipeRequest request, ServerCallContext context)
    {
        if (request.AccountId == null || request.RecipeIds == null || request.RecipeIds.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "RecipeIds and AccountId must not be null or empty."));
        }
        var recipeIdSet = request.RecipeIds.Select(Guid.Parse).ToHashSet();
        var accountId = Guid.Parse(request.AccountId);

        if (accountId == Guid.Empty || recipeIdSet == null || recipeIdSet.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "RecipeIds and AccountId must not be null or empty."));
        }

        var response = await _sender.Send(new GetSimpleRecipesQuery
        {
            AccountId = accountId,
            RecipeIds = recipeIdSet,
        });
        response.ThrowIfFailure();

        var recipes = response.Value!;

        var mapRecipe = recipes.ToDictionary(r => r.Id);

        var mapField = new MapField<string, GrpcSimpleRecipe>();
        foreach (var (key, value) in mapRecipe)
        {
            mapField[key.ToString()] = new GrpcSimpleRecipe
            {
                Id = value.Id.ToString(),
                AuthorId = value.AuthorId.ToString(),
                AuthorAvtUrl = value.AuthorAvtUrl,
                AuthorDisplayName = value.AuthorDisplayName,
                AuthorUsername = value.AuthorUsername,
                Title = value.Title,
                Description = value.Description,
                RecipeImgUrl = value.RecipeImgUrl,
                NumberOfComment = value.NumberOfComment,
                VoteDiff = value.VoteDiff,
                Vote = value.Vote.ToString(),
                CreatedAt = value.CreatedAt.ToTimestamp(),
                UpdatedAt = value.UpdatedAt.ToTimestamp(),
            };
        }

        var grpcResult = new GrpcMapSimpleRecipes
        {
            Recipes = { mapField }
        };

        _logger.LogInformation("Grpc GetSimpleRecipes successfully!");
        return grpcResult;
    }

    public override async Task<GrpcMapSimpleRecipes> SearchSimpleRecipes(GrpcSearchSimpleRecipeRequest request, ServerCallContext context)
    {
        if (request.AccountId == null || request.RecipeIds == null || request.RecipeIds.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "RecipeIds and AccountId must not be null or empty."));
        }
        var recipeIdSet = request.RecipeIds.Select(Guid.Parse).ToHashSet();
        var accountId = Guid.Parse(request.AccountId);

        if (accountId == Guid.Empty || recipeIdSet == null || recipeIdSet.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "RecipeIds and AccountId must not be null or empty."));
        }

        var response = await _sender.Send(new SearchSimpleRecipesQuery
        {
            AccountId = accountId,
            RecipeIds = recipeIdSet,
            Keyword = request.Keyword
        });
        response.ThrowIfFailure();

        var recipes = response.Value!;

        var mapRecipe = recipes.ToDictionary(r => r.Id);

        var mapField = new MapField<string, GrpcSimpleRecipe>();
        foreach (var (key, value) in mapRecipe)
        {
            mapField[key.ToString()] = new GrpcSimpleRecipe
            {
                Id = value.Id.ToString(),
                AuthorId = value.AuthorId.ToString(),
                AuthorAvtUrl = value.AuthorAvtUrl,
                AuthorDisplayName = value.AuthorDisplayName,
                AuthorUsername = value.AuthorUsername,
                Title = value.Title,
                Description = value.Description,
                RecipeImgUrl = value.RecipeImgUrl,
                NumberOfComment = value.NumberOfComment,
                VoteDiff = value.VoteDiff,
                Vote = value.Vote.ToString(),
                CreatedAt = value.CreatedAt.ToTimestamp(),
                UpdatedAt = value.UpdatedAt.ToTimestamp()
            };
        }

        var grpcResult = new GrpcMapSimpleRecipes
        {
            Recipes = { mapField }
        };

        _logger.LogInformation("Grpc SearchSimpleRecipes successfully!");
        return grpcResult;
    }

    public override async Task<GrpcMapSimpleComments> GetSimpleComments(GrpcGetSimpleCommentRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(new GetCommentDetailQuery
        {
            RecipeAndCommentIdSet = request.Ids.ToHashSet(),
        });

        result.ThrowIfFailure();
        var mapField = new MapField<string, GrpcSimpleComment>();
        foreach (var (key, simpleComment) in result.Value!)
        {
            if (simpleComment == null)
            {
                throw new NullReferenceException("Simple comment is null");
            }
            mapField.Add(key, new GrpcSimpleComment
            {
                Id = simpleComment.Id.ToString(),
                AuthorAvatarURL = simpleComment.AuthorAvatarURL,
                AuthorDisplayName = simpleComment.AuthorDisplayName,
                AuthorId = simpleComment.AuthorId.ToString(),
                AuthorUsername = simpleComment.AuthorUsername,
                Content = simpleComment.Content,
                CreatedAt = simpleComment.CreatedAt.ToTimestamp(),
                UpdatedAt = simpleComment.UpdatedAt.ToTimestamp(),
                IsActive = simpleComment.IsActive
            });
        }
        var grpcResult = new GrpcMapSimpleComments
        {
            Comments = { mapField }
        };

        return grpcResult;
    }

    public override async Task<GrpcMapCommentReports> GetCommentReports(GrpcGetCommentReportRequest request, ServerCallContext context)
    {
        var set = request.Ids.Select(Guid.Parse).ToHashSet();

        var result = await _sender.Send(new GetCommentReportDetailByHashSetQuery
        {
            Lang = request.Lang,
            ReportIds = set
        });
        result.ThrowIfFailure();

        var mapField = new MapField<string, GrpcCommentReportResponse>();
        foreach (var (k, v) in result.Value!)
        {
            RepeatedField<string> reasonRepeatedField = [.. v.Report.Reasons];

            mapField.Add(k.ToString(), new GrpcCommentReportResponse
            {
                Reporter = new CommonProto.GrpcSimpleUser
                {
                    AccountId = v.Reporter.AccountId.ToString(),
                    AccountUsername = v.Reporter.AccountUsername,
                    AvtUrl = v.Reporter.AvtUrl,
                    DisplayName = v.Reporter.DisplayName
                },
                Comment = new GrpcSimpleComment
                {
                    Id = v.Comment.Id.ToString(),
                    AuthorAvatarURL = v.Comment.AuthorAvatarURL,
                    AuthorDisplayName = v.Comment.AuthorDisplayName,
                    AuthorId = v.Comment.Id.ToString(),
                    AuthorUsername = v.Comment.AuthorUsername,
                    Content = v.Comment.Content,
                    IsActive = v.Comment.IsActive,
                    CreatedAt = v.Comment.CreatedAt.ToTimestamp(),
                    UpdatedAt = v.Comment.UpdatedAt.ToTimestamp()
                },
                Recipe = new GrpcSimpleRecipe
                {
                    Id = v.Recipe.Id.ToString(),
                    AuthorAvtUrl = v.Recipe.AuthorAvatarURL,
                    AuthorDisplayName = v.Recipe.AuthorDisplayName,
                    UpdatedAt = v.Recipe.UpdatedAt.ToTimestamp(),
                    CreatedAt = v.Recipe.CreatedAt.ToTimestamp(),
                    AuthorId = v.Recipe.AuthorId.ToString(),
                    AuthorUsername = v.Recipe.AuthorUsername,
                    Description = v.Recipe.Description,
                    Title = v.Recipe.Title,
                    RecipeImgUrl = v.Recipe.RecipeImageUrl,
                    //Not need mapping this right now
                    VoteDiff = 0,
                    NumberOfComment = 0,
                    Vote = ""
                },
                Report = new CommonProto.GrpcSimpleReport
                {
                    Id = v.Report.Id.ToString(),
                    AdditionalDetail = v.Report.AdditionalDetail,
                    ReporterAccountId = v.Report.ReporterAccountId.ToString(),
                    CreatedAt = v.Report.CreatedAt.ToTimestamp(),
                    Reasons = { reasonRepeatedField },
                    Status = v.Report.Status
                }
            });
        }

        return new GrpcMapCommentReports
        {
            CommentReports = { mapField }
        };
    }

    public override async Task<GrpcMapRecipeReports> GetRecipeReports(GrpcGetRecipeReportRequest request, ServerCallContext context)
    {
        var set = request.Ids.Select(Guid.Parse).ToHashSet();

        var result = await _sender.Send(new GetRecipeReportDetailByHashSetQuery
        {
            Lang = request.Lang,
            ReportIds = set
        });
        result.ThrowIfFailure();

        var mapField = new MapField<string, GrpcRecipeReportResponse>();
        foreach (var (k, v) in result.Value!)
        {
            RepeatedField<string> reasonRepeatedField = [.. v.Report.Reasons];

            mapField.Add(k.ToString(), new GrpcRecipeReportResponse
            {
                Reporter = new CommonProto.GrpcSimpleUser
                {
                    AccountId = v.Reporter.AccountId.ToString(),
                    DisplayName = v.Reporter.DisplayName,
                    AccountUsername = v.Reporter.AccountUsername,
                    AvtUrl = v.Reporter.AvtUrl
                },
                Recipe = new GrpcSimpleRecipe
                {
                    Id = v.Recipe.Id.ToString(),
                    AuthorAvtUrl = v.Recipe.AuthorAvatarURL,
                    AuthorDisplayName = v.Recipe.AuthorDisplayName,
                    UpdatedAt = v.Recipe.UpdatedAt.ToTimestamp(),
                    CreatedAt = v.Recipe.CreatedAt.ToTimestamp(),
                    AuthorId = v.Recipe.AuthorId.ToString(),
                    AuthorUsername = v.Recipe.AuthorUsername,
                    Description = v.Recipe.Description,
                    Title = v.Recipe.Title,
                    RecipeImgUrl = v.Recipe.RecipeImageUrl,
                    //Not need mapping this right now
                    VoteDiff = 0,
                    NumberOfComment = 0,
                    Vote = ""
                },
                Report = new CommonProto.GrpcSimpleReport
                {
                    Id = v.Report.Id.ToString(),
                    AdditionalDetail = v.Report.AdditionalDetail,
                    CreatedAt = v.Report.CreatedAt.ToTimestamp(),
                    ReporterAccountId = v.Report.ReporterAccountId.ToString(),
                    Reasons = { reasonRepeatedField },
                    Status = v.Report.Status
                }
            });
        }

        return new GrpcMapRecipeReports
        {
            RecipeReports = { mapField }
        };
    }

    public override async Task<GrpcMapTagResponse> GetTags(GrpcGetTagsRequest request, ServerCallContext context)
    {
        var hashSet = request.Ids.Select(Guid.Parse).ToHashSet();
        var result = await _sender.Send(new GetTagsByHashSetQuery
        {
            TagIds = hashSet,
        });
        result.ThrowIfFailure();

        var mapField = new MapField<string, GrpcTagDTO>();
        foreach (var (k, v) in result.Value)
        {
            mapField.Add(k.ToString(), new GrpcTagDTO
            {
                Id = v.Id.ToString(),
                Category = v.Category.ToString(),
                Code = v.Code,
                ImageUrl = v.ImageUrl,
                Status = v.Status.ToString(),
                En = v.Value.En ?? "",
                Vi = v.Value.Vi ?? "",
            });
        }

        return new GrpcMapTagResponse
        {
            Tags = { mapField }
        };
    }
}
