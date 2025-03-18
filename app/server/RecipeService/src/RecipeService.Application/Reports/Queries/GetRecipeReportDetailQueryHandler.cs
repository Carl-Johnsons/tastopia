
using AutoMapper;
using Contract.Constants;
using Contract.DTOs.UserDTO;
using Contract.Utilities;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.Reports.Queries;

public record GetRecipeReportDetailQuery : IRequest<Result<AdminReportRecipeDetailResponse>>
{
    public string Lang { get; init; } = "en";
    public Guid RecipeId { get; init; }
}

public class GetRecipeReportDetailQueryHandler : IRequestHandler<GetRecipeReportDetailQuery, Result<AdminReportRecipeDetailResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IMapper _mapper;

    public GetRecipeReportDetailQueryHandler(IApplicationDbContext context, GrpcUser.GrpcUserClient grpcUserClient, IMapper mapper)
    {
        _context = context;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
    }

    public async Task<Result<AdminReportRecipeDetailResponse>> Handle(GetRecipeReportDetailQuery request, CancellationToken cancellationToken)
    {
        var userReportCollection = _context.GetDatabase().GetCollection<UserReportRecipe>(nameof(UserReportRecipe));
        var recipeCollection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe));
        var recipeTagCollection = _context.GetDatabase()
            .GetCollection<RecipeTag>(nameof(RecipeTag))
            .AsQueryable();

        var tagCollection = _context.GetDatabase()
            .GetCollection<Domain.Entities.Tag>(nameof(Domain.Entities.Tag))
            .AsQueryable();

        var tagQuery = from rt in recipeTagCollection
                       join t in tagCollection on rt.TagId equals t.Id
                       where rt.RecipeId == request.RecipeId && t.Status == TagStatus.Active
                       select t;
        var tagList = tagQuery.ToList();

        var normalizedLangue = LanguageUtility.ToIso6391(request.Lang);

        var pipeline = recipeCollection.Aggregate()
            .Match(r => r.Id == request.RecipeId)
            .Lookup<Recipe, UserReportRecipe, RecipeWithReportList>(
                foreignCollection: userReportCollection,
                localField: r => r.Id,
                foreignField: ur => ur.EntityId,
                @as: rwrl => rwrl.UserReportRecipes
            )
            .Project(r => new RecipeWithReportList
            {
                Id = r.Id,
                AuthorId = r.AuthorId,
                Ingredients = r.Ingredients,
                ImageUrl = r.ImageUrl,
                IsActive = r.IsActive,
                Description = r.Description,
                CookTime = r.CookTime,
                RecipeTags = r.RecipeTags,
                VoteDiff = r.VoteDiff,
                Steps = r.Steps,
                Serves = r.Serves,
                UserReportRecipes = r.UserReportRecipes,
                NumberOfComment = r.NumberOfComment,
                Title = r.Title,
                TotalView = r.TotalView,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt,
            })
            .SingleOrDefaultAsync(cancellationToken);

        var result = await pipeline;

        var accountIds = result.UserReportRecipes.Select(r => r.AccountId).ToList();

        var repeatedField = _mapper.Map<RepeatedField<string>>(accountIds);
        var mapUserGrpc = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { repeatedField }
        }, cancellationToken: cancellationToken);

        var grpcGetAccountDetail = await _grpcUserClient.GetUserDetailAsync(new GrpcAccountIdRequest
        {
            AccountId = result.AuthorId.ToString()
        });

        var reportDetailResponse = new AdminReportRecipeDetailResponse
        {
            Recipe = new Recipe
            {
                Id = result.Id,
                AuthorId = result.AuthorId,
                CookTime = result.CookTime,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt,
                Description = result.Description,
                ImageUrl = result.ImageUrl,
                Ingredients = result.Ingredients,
                IsActive = result.IsActive,
                Serves = result.Serves,
                TotalView = result.TotalView,
                VoteDiff = result.VoteDiff,
                Steps = result.Steps,
                Title = result.Title,
                NumberOfComment = result.NumberOfComment
            },
            AuthorAvtUrl = grpcGetAccountDetail.AvatarUrl,
            AuthorDisplayName = grpcGetAccountDetail.DisplayName,
            AuthorUsername = grpcGetAccountDetail.AccountUsername,
            AuthorNumberOfFollower = grpcGetAccountDetail.TotalFollower ?? 0,
            Tags = tagList,
            Reports = result.UserReportRecipes.OrderByDescending(urr => urr.CreatedAt).Select(urr => new ReportRecipeResponse
            {
                Id = urr.Id,
                AdditionalDetail = urr.AdditionalDetails,
                CreatedAt = urr.CreatedAt,
                Status = urr.Status.ToString(),
                ReporterUsername = mapUserGrpc.Users[urr.AccountId.ToString()].AccountUsername,
                ReporterAvtUrl = mapUserGrpc.Users[urr.AccountId.ToString()].AvtUrl,
                Reasons = ReportReasonData.RecipeReportReasons.Where(rrr => urr.ReasonCodes.Contains(rrr.Code))
                                                              .Select(rrr => request.Lang == LanguageValidation.Vi ? rrr.Vi : rrr.En)
                                                              .ToList(),
            }).ToList()
        };


        return Result<AdminReportRecipeDetailResponse>.Success(reportDetailResponse);
    }

    private class RecipeWithReportList : Recipe
    {
        public List<UserReportRecipe> UserReportRecipes { get; set; } = [];
    }
}
