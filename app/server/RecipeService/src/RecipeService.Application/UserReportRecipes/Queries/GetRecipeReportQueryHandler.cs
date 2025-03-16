using AutoMapper;
using Contract.Constants;
using Contract.DTOs;
using Contract.Utilities;
using Google.Protobuf.Collections;
using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.UserReportRecipes.Queries;
public record GetRecipeReportsQuery : IRequest<Result<PaginatedAdminReportRecipeListResponse>>
{
    public string Lang { get; init; } = "en";
    public PaginatedDTO? paginatedDTO { get; init; } = null!;
}

public class GetRecipeReportQueryHandler : IRequestHandler<GetRecipeReportsQuery, Result<PaginatedAdminReportRecipeListResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<AdminReportRecipeResponse, NumberedPaginatedMetadata> _paginateDataUtility;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IMapper _mapper;

    public GetRecipeReportQueryHandler(IApplicationDbContext context,
                                       IPaginateDataUtility<AdminReportRecipeResponse, NumberedPaginatedMetadata> paginateDataUtility,
                                       GrpcUser.GrpcUserClient grpcUserClient,
                                       IMapper mapper)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedAdminReportRecipeListResponse>> Handle(GetRecipeReportsQuery request, CancellationToken cancellationToken)
    {
        var keyword = request.paginatedDTO?.Keyword;

        var userReportCollection = _context.GetDatabase().GetCollection<UserReportRecipe>(nameof(UserReportRecipe));
        var recipeCollection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe));
        var normalizedLangue = LanguageUtility.ToIso6391(request.Lang);

        var pipeline = userReportCollection.Aggregate()
            .Lookup<UserReportRecipe, Recipe, UserReportWithRecipe>(foreignCollection: recipeCollection,
                                                                    localField: report => report.RecipeId,
                                                                    foreignField: recipe => recipe.Id,
                                                                    @as: result => result.Recipe)
            .Unwind<UserReportWithRecipe, UserReportWithRecipe>(result => result.Recipe);

        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            var searchUserResponse = await _grpcUserClient.SearchUserAsync(new GrpcSearchUserRequest
            {
                Keyword = keyword,
            }, cancellationToken: cancellationToken);


            var searchAuthorIds = searchUserResponse.AccountIds.ToHashSet();
            var searchRecipeReportReasonCode = ReportReasonData.RecipeReportReasons.Where(rrr => normalizedLangue
                                                                                                 == LanguageValidation.Vi ? rrr.Vi.Contains(keyword) : rrr.En.Contains(keyword))
                                                                                   .Select(rrr => rrr.Code)
                                                                                   .ToList();
            pipeline = pipeline.Match(report =>
                report.Recipe!.Title.Contains(keyword) ||
                (searchAuthorIds != null && searchAuthorIds.Contains(report.Recipe.AuthorId.ToString())) ||
                (searchAuthorIds != null && searchAuthorIds.Contains(report.AccountId.ToString())) ||
                searchRecipeReportReasonCode.Any(srrc => report.ReasonCodes.Contains(srrc))
            );
        }

        var userReportList = await pipeline.ToListAsync(cancellationToken);

        var accountId = userReportList.Select(ur => ur.AccountId).Concat(userReportList.Select(ur => ur.Recipe!.AuthorId)).ToList();
        var repeatedField = _mapper.Map<RepeatedField<string>>(accountId);

        var mapGrpcUserResponse = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { repeatedField },
        }, cancellationToken: cancellationToken);

        var totalPage = (userReportList.Count() + RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT - 1) / RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT;

        var adminReportRecipe = userReportList.Select(ur => new AdminReportRecipeResponse
        {
            RecipeId = ur.RecipeId,
            RecipeImageURL = ur.Recipe!.ImageUrl,
            RecipeTitle = ur.Recipe.Title,
            Status = ur.Status.ToString(),
            CreatedAt = ur.CreatedAt,
            RecipeOwnerUsername = mapGrpcUserResponse.Users[ur.Recipe.AuthorId.ToString()].AccountUsername,
            ReporterUsername = mapGrpcUserResponse.Users[ur.AccountId.ToString()].AccountUsername,
            ReportReason = string.Join(", ", ReportReasonData.RecipeReportReasons.Where(rrr => ur.ReasonCodes.Contains(rrr.Code))
                                                                                 .Select(rrr => normalizedLangue == LanguageValidation.Vi ? rrr.Vi : rrr.En)
                                                                                 .ToList()),
        }).AsQueryable();

        var paginatedQuery = _paginateDataUtility.PaginateQuery(adminReportRecipe, new PaginateParam
        {
            Limit = RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT,
            Offset = (request.paginatedDTO?.Skip ?? 0) * RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT,
            SortBy = request.paginatedDTO?.SortBy ?? "CreatedAt",
            SortOrder = request.paginatedDTO?.SortOrder ?? SortType.DESC
        });

        var paginatedResult = paginatedQuery.ToList();

        var adminReportRecipeResponse = new PaginatedAdminReportRecipeListResponse
        {
            PaginatedData = paginatedResult,
            Metadata = new NumberedPaginatedMetadata
            {
                CurrentPage = (request.paginatedDTO?.Skip ?? 0) + 1,
                TotalPage = totalPage
            }
        };

        return Result<PaginatedAdminReportRecipeListResponse>.Success(adminReportRecipeResponse);
    }

    private class UserReportWithRecipe : UserReportRecipe
    {
        public Recipe? Recipe { get; set; }
    }
}
