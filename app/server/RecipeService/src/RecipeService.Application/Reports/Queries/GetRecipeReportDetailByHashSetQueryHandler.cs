
using AutoMapper;
using Contract.Constants;
using Contract.Utilities;
using Google.Protobuf.Collections;
using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.Reports.Queries;
public record GetRecipeReportDetailByHashSetQuery : IRequest<Result<Dictionary<Guid, AdminSingleRecipeCommentDetailResponse>?>>
{
    public string Lang { get; init; } = null!;
    public HashSet<Guid> ReportIds { get; init; } = null!;
}


public class GetRecipeReportDetailByHashSetQueryHandler : IRequestHandler<GetRecipeReportDetailByHashSetQuery, Result<Dictionary<Guid, AdminSingleRecipeCommentDetailResponse>?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public GetRecipeReportDetailByHashSetQueryHandler(IApplicationDbContext context,
                                              GrpcUser.GrpcUserClient grpcUserClient,
                                              IMapper mapper)
    {
        _context = context;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
    }

    public async Task<Result<Dictionary<Guid, AdminSingleRecipeCommentDetailResponse>?>> Handle(GetRecipeReportDetailByHashSetQuery request,
                                                                                                CancellationToken cancellationToken)
    {
        var normalizedLangue = LanguageUtility.ToIso6391(request.Lang);

        var userReportRecipeCollection = _context.GetDatabase().GetCollection<UserReportRecipe>(nameof(UserReportRecipe));
        var recipeCollection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe));

        var pipeline = userReportRecipeCollection.Aggregate()
            .Match(urc => request.ReportIds.Contains(urc.Id))
            .Lookup<UserReportRecipe, Recipe, CommentReportWithRecipe>(foreignCollection: recipeCollection,
                                                                     localField: urc => urc.EntityId,
                                                                     foreignField: r => r.Id,
                                                                     @as: rwrl => rwrl.Recipe)
            .Unwind<CommentReportWithRecipe, CommentReportWithRecipe>(rwrl => rwrl.Recipe)
            .Project(r => new CommentReportWithRecipe
            {
                Recipe = new Recipe
                {
                    Id = r.Recipe.Id,
                    Description = r.Recipe.Description,
                    AuthorId = r.Recipe.AuthorId,
                    CreatedAt = r.Recipe.CreatedAt,
                    UpdatedAt = r.Recipe.UpdatedAt,
                    CookTime = r.Recipe.CookTime,
                    ImageUrl = r.Recipe.ImageUrl,
                    IsActive = r.Recipe.IsActive,
                    Title = r.Recipe.Title,
                    VoteDiff = r.Recipe.VoteDiff,
                    Ingredients = r.Recipe.Ingredients,
                },
                Id = r.Id,
                AccountId = r.AccountId,
                AdditionalDetails = r.AdditionalDetails,
                CreatedAt = r.CreatedAt,
                EntityId = r.EntityId,
                ReasonCodes = r.ReasonCodes,
                RecipeId = r.Id,
                Status = r.Status,
                UpdatedAt = r.UpdatedAt,
            })
            .ToListAsync(cancellationToken);

        var result = await pipeline;
        if (result == null || result.Count == 0)
        {
            return Result<Dictionary<Guid, AdminSingleRecipeCommentDetailResponse>?>.Failure(ReportError.NotFound);
        }
        List<Guid> accountIds = result.Select(r => r.AccountId)
            .Union(result.Select(r => r.Recipe.AuthorId))
            .ToList();

        var repeatedField = _mapper.Map<RepeatedField<string>>(accountIds);
        var mapUserGrpc = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { repeatedField }
        }, cancellationToken: cancellationToken);

        var reportDict = result.ToDictionary(r => r.Id, report =>
        {
            var reporter = mapUserGrpc.Users[report.AccountId.ToString()];

            return new AdminSingleRecipeCommentDetailResponse
            {
                Reporter = new Contract.DTOs.UserDTO.SimpleUser
                {
                    AccountId = Guid.Parse(reporter.AccountId),
                    AccountUsername = reporter.AccountUsername,
                    AvtUrl = reporter.AvtUrl,
                    DisplayName = reporter.DisplayName,
                },
                Recipe = new AdminRecipeResponse
                {
                    Id = report.Recipe.Id,
                    AuthorId = report.Recipe.AuthorId,
                    AuthorAvatarURL = mapUserGrpc.Users[report.Recipe.AuthorId.ToString()].AvtUrl,
                    AuthorDisplayName = mapUserGrpc.Users[report.Recipe.AuthorId.ToString()].DisplayName,
                    AuthorUsername = mapUserGrpc.Users[report.Recipe.AuthorId.ToString()].AccountUsername,
                    CreatedAt = report.Recipe.CreatedAt,
                    Ingredients = string.Join(", ", report.Recipe.Ingredients),
                    IsActive = report.Recipe.IsActive,
                    RecipeImageUrl = report.Recipe.ImageUrl,
                    Description = report.Recipe.Description,
                    UpdatedAt = report.Recipe.UpdatedAt,
                    Title = report.Recipe.Title
                },
                Report = new SimpleReportResponse
                {
                    Id = report.Id,
                    AdditionalDetail = report.AdditionalDetails,
                    CreatedAt = report.CreatedAt,
                    Status = report.Status.ToString(),
                    ReporterAccountId = report.AccountId,
                    Reasons = ReportReasonData.RecipeReportReasons.Where(rrr => report.ReasonCodes.Contains(rrr.Code))
                                                              .Select(rrr => request.Lang == LanguageValidation.Vi ? rrr.Vi : rrr.En)
                                                              .ToList(),
                }
            };
        });

        return Result<Dictionary<Guid, AdminSingleRecipeCommentDetailResponse>?>.Success(reportDict);
    }

    private class CommentReportWithRecipe : UserReportComment
    {
        public Recipe Recipe { get; set; } = null!;
    }
}
