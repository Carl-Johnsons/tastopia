using AutoMapper;
using Google.Protobuf.Collections;
using RecipeProto;
using TrackingService.Domain.Entities;
using TrackingService.Domain.Errors;
using TrackingService.Domain.Responses;

namespace TrackingService.Application.UserViewRecipeDetails.Queries;

public class SearchUserViewRecipeDetaiQuery : IRequest<Result<PaginatedUserViewRecipeDetailListResponse?>>
{
    public Guid AccountId { get; set; }

    public int Skip { get; set;}

    public string? Keyword { get; set; } = null!;
}

public class SearchUserViewRecipeDetaiQueryHandler : IRequestHandler<SearchUserViewRecipeDetaiQuery, Result<PaginatedUserViewRecipeDetailListResponse?>>
{
    private readonly GrpcRecipe.GrpcRecipeClient _grpcRecipeClient;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<UserViewRecipeDetail, AdvancePaginatedMetadata> _paginateDataUtility;


    public SearchUserViewRecipeDetaiQueryHandler(GrpcRecipe.GrpcRecipeClient grpcRecipeClient, IMapper mapper, IApplicationDbContext context, IPaginateDataUtility<UserViewRecipeDetail, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _grpcRecipeClient = grpcRecipeClient;
        _mapper = mapper;
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedUserViewRecipeDetailListResponse?>> Handle(SearchUserViewRecipeDetaiQuery request, CancellationToken cancellationToken)
    {
        var keyword = request.Keyword == null ? "" : request.Keyword;
        var skip = request.Skip;
        var accountId = request.AccountId;
        if (accountId == Guid.Empty)
        {
            return Result<PaginatedUserViewRecipeDetailListResponse?>.Failure(UserViewRecipeDetailError.NotFound, "ACCOUNT ID NULL");
        }
        var viewsQuery = _context.UserViewRecipeDetails.Where(v => v.AccountId == accountId).OrderByDescending(v => v.UpdatedAt).AsQueryable();

        var views = viewsQuery.ToHashSet();
        var viewsMap = viewsQuery.ToDictionary(v => v.RecipeId.ToString());

        if (views == null || views.Count == 0)
        {

            return Result<PaginatedUserViewRecipeDetailListResponse?>.Success(new PaginatedUserViewRecipeDetailListResponse
            {
                PaginatedData = [],
                Metadata = new AdvancePaginatedMetadata
                {
                    HasNextPage = false,
                    TotalPage = 0,
                }
            });
        }

        var response = await _grpcRecipeClient.SearchSimpleRecipesAsync(new GrpcSearchSimpleRecipeRequest
        {
            AccountId = accountId.ToString(),
            RecipeIds = { _mapper.Map<RepeatedField<string>>(views.Select(v => v.RecipeId).ToHashSet()) },
            Keyword = keyword,
        }, cancellationToken: cancellationToken);


        if (response.Recipes == null || response.Recipes.Count == 0)
        {
            return Result<PaginatedUserViewRecipeDetailListResponse?>.Success(new PaginatedUserViewRecipeDetailListResponse
            {
                PaginatedData = [],
                Metadata = new AdvancePaginatedMetadata
                {
                    HasNextPage = false,
                    TotalPage = 0,
                }
            });
        }

        var mapRecipe = response.Recipes
            .Skip(UserViewRecipeDetailConstant.USER_VIEW_RECIPE_DETAIL_LIMIT * skip)
            .Take(UserViewRecipeDetailConstant.USER_VIEW_RECIPE_DETAIL_LIMIT).ToDictionary();

        var totalPage = (response.Recipes.Count() + UserViewRecipeDetailConstant.USER_VIEW_RECIPE_DETAIL_LIMIT - 1) / UserViewRecipeDetailConstant.USER_VIEW_RECIPE_DETAIL_LIMIT;


        var listView = new List<SimpleRecipeResponse>();

        foreach (var (key, value) in mapRecipe)
        {
            var view = new SimpleRecipeResponse
            {
                Id = Guid.Parse(value.Id),
                AuthorId = Guid.Parse(value.AuthorId),
                Title = value.Title,
                Description = value.Description,
                RecipeImgUrl = value.RecipeImgUrl,
                AuthorAvtUrl = value.AuthorAvtUrl,
                AuthorDisplayName = value.AuthorDisplayName,
                NumberOfComment = value.NumberOfComment,
                VoteDiff = value.VoteDiff,
                Vote = value.Vote,
                CreatedAt = viewsMap[value.Id].CreatedAt,
                UpdatedAt = viewsMap[value.Id].UpdatedAt
            };
            listView.Add(view);
        }

        var hasNextPage = true;

        if (skip >= totalPage - 1)
        {
            hasNextPage = false;
        }

        var result = new PaginatedUserViewRecipeDetailListResponse
        {
            PaginatedData = listView,
            Metadata = new AdvancePaginatedMetadata
            {
                HasNextPage = hasNextPage,
                TotalPage = totalPage,
            }
        };
        return Result<PaginatedUserViewRecipeDetailListResponse?>.Success(result);
    }
}
