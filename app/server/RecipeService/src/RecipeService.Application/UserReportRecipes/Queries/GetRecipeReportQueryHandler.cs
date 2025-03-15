using AutoMapper;
using Contract.Constants;
using Contract.DTOs;
using Contract.DTOs.UserDTO;
using Contract.Utilities;
using Google.Protobuf.Collections;
using MongoDB.Bson.Serialization;
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
    private readonly IPaginateDataUtility<UserReportWithRecipe, NumberedPaginatedMetadata> _paginateDataUtility;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IMapper _mapper;

    public GetRecipeReportQueryHandler(IApplicationDbContext context,
                                       IPaginateDataUtility<UserReportWithRecipe, NumberedPaginatedMetadata> paginateDataUtility,
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
        var userReportCollection = _context.GetDatabase().GetCollection<UserReportRecipe>(nameof(UserReportRecipe));
        var recipeCollection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe));
        var normalizedLangue = LanguageUtility.ToIso6391(request.Lang);

        var pipeline = userReportCollection.Aggregate()
            .Lookup<UserReportRecipe, Recipe, UserReportWithRecipe>(foreignCollection: recipeCollection,
                                                                    localField: report => report.RecipeId,
                                                                    foreignField: recipe => recipe.Id,
                                                                    @as: result => result.Recipe)
            .Unwind(result => result.Recipe);

        var bsonResults = await pipeline.ToListAsync(cancellationToken);
        var userReportList = bsonResults.Select(doc => BsonSerializer.Deserialize<UserReportWithRecipe>(doc)).ToList().AsQueryable();

        var accountId = userReportList.Select(ur => ur.AccountId).Concat(userReportList.Select(ur => ur.Recipe!.AuthorId)).ToList();
        var repeatedField = _mapper.Map<RepeatedField<string>>(accountId);

        var mapGrpcUserResponse = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { repeatedField },
        }, cancellationToken: cancellationToken);

        var mapUser = new Dictionary<Guid, SimpleUser>();
        foreach (var (key, value) in mapGrpcUserResponse.Users)
        {
            mapUser.Add(Guid.Parse(key), new SimpleUser
            {
                AccountId = Guid.Parse(value.AccountId),
                AccountUsername = value.AccountUsername,
                AvtUrl = value.AvtUrl,
                DisplayName = value.DisplayName
            });
        }

        var totalPage = (userReportList.Count() + RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT - 1) / RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT;

        var paginatedQuery = _paginateDataUtility.PaginateQuery(userReportList, new PaginateParam
        {
            Limit = RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT,
            Offset = (request.paginatedDTO?.Skip ?? 0) * RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT,
            SortBy = request.paginatedDTO?.SortBy ?? "CreatedAt",
            SortOrder = request.paginatedDTO?.SortOrder ?? SortType.DESC
        });

        var paginatedResult = paginatedQuery.Select(pq => new AdminReportRecipeResponse
        {
            RecipeId = pq.RecipeId,
            RecipeImageURL = pq.Recipe!.ImageUrl,
            RecipeTitle = pq.Recipe.Title,
            Status = pq.Status.ToString(),
            CreatedAt = pq.CreatedAt,
            RecipeOwnerUsername = mapUser[pq.Recipe.AuthorId].AccountUsername,
            ReporterUsername = mapUser[pq.AccountId].AccountUsername,
            ReportReason = string.Join(", ", ReportReasonData.RecipeReportReasons.Where(rrr => pq.ReasonCodes.Contains(rrr.Code))
                                                                                 .Select(rrr => normalizedLangue == LanguageValidation.Vi ? rrr.Vi : rrr.En)
                                                                                 .ToList()),
        }).ToList();

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

    public class UserReportWithRecipe : UserReportRecipe
    {
        public Recipe? Recipe { get; set; }
    }
}
