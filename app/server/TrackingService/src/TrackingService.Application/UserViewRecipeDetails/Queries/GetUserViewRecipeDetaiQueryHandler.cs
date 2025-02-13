using AutoMapper;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeProto;
using TrackingService.Domain.Entities;
using TrackingService.Domain.Errors;
using TrackingService.Domain.Responses;

namespace TrackingService.Application.UserViewRecipeDetails.Queries;

public class GetUserViewRecipeDetaiQuery : IRequest<Result<PaginatedUserViewRecipeDetailListResponse?>>
{
    public Guid AccountId { get; set; }

    public int Skip { get; set;}
}

public class GetUserViewRecipeDetaiQueryHandler : IRequestHandler<GetUserViewRecipeDetaiQuery, Result<PaginatedUserViewRecipeDetailListResponse?>>
{
    private readonly GrpcRecipe.GrpcRecipeClient _grpcRecipeClient;
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<UserViewRecipeDetail, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly ILogger<GetUserViewRecipeDetaiQueryHandler> _logger;


    public GetUserViewRecipeDetaiQueryHandler(GrpcRecipe.GrpcRecipeClient grpcRecipeClient, IMapper mapper, IApplicationDbContext context, IPaginateDataUtility<UserViewRecipeDetail, AdvancePaginatedMetadata> paginateDataUtility, ILogger<GetUserViewRecipeDetaiQueryHandler> logger)
    {
        _grpcRecipeClient = grpcRecipeClient;
        _mapper = mapper;
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _logger = logger;
    }

    public async Task<Result<PaginatedUserViewRecipeDetailListResponse?>> Handle(GetUserViewRecipeDetaiQuery request, CancellationToken cancellationToken)
    {
        var skip = request.Skip;
        var accountId = request.AccountId;
        if (accountId == Guid.Empty)
        {
            return Result<PaginatedUserViewRecipeDetailListResponse?>.Failure(UserViewRecipeDetailError.NotFound, "ACCOUNT ID NULL");
        }
        var viewsQuery = _context.UserViewRecipeDetails.Where(v => v.AccountId == accountId).OrderByDescending(v => v.UpdatedAt).AsQueryable();
        var totalPage = (await viewsQuery.CountAsync() + UserViewRecipeDetailConstant.USER_VIEW_RECIPE_DETAIL_LIMIT - 1) / UserViewRecipeDetailConstant.USER_VIEW_RECIPE_DETAIL_LIMIT;
        viewsQuery = _paginateDataUtility.PaginateQuery(viewsQuery, new PaginateParam
        {
            Offset = skip * UserViewRecipeDetailConstant.USER_VIEW_RECIPE_DETAIL_LIMIT,
            Limit = UserViewRecipeDetailConstant.USER_VIEW_RECIPE_DETAIL_LIMIT
        });

        var views = viewsQuery.ToHashSet();

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

        var response = await _grpcRecipeClient.GetSimpleRecipesAsync(new GrpcGetSimpleRecipeRequest
        {
            AccountId = accountId.ToString(),
            RecipeIds = { _mapper.Map<RepeatedField<string>>(views.Select(v => v.RecipeId).ToHashSet()) }
        }, cancellationToken: cancellationToken);


        if (response.Recipes == null || response.Recipes.Count != views.Count)
        {
            _logger.LogInformation("========================================================");
            _logger.LogInformation("Recipe:" + response.Recipes.Count + "| view:" + views.Count);
            _logger.LogInformation("========================================================");

            return Result<PaginatedUserViewRecipeDetailListResponse?>.Failure(UserViewRecipeDetailError.NotFound, "Not found simple recipes");
        }

        var mapRecipe = response.Recipes;

        var listView = new List<SimpleRecipeResponse>();

        foreach (var v in views)
        {
            var redipeId = v.RecipeId.ToString();
            var view = new SimpleRecipeResponse
            {
                Id = Guid.Parse(mapRecipe[redipeId].Id),
                AuthorId = Guid.Parse(mapRecipe[redipeId].AuthorId),
                Title = mapRecipe[redipeId].Title,
                Description = mapRecipe[redipeId].Description,
                RecipeImgUrl = mapRecipe[redipeId].RecipeImgUrl,
                AuthorAvtUrl = mapRecipe[redipeId].AuthorAvtUrl,
                AuthorDisplayName = mapRecipe[redipeId].AuthorDisplayName,
                NumberOfComment = mapRecipe[redipeId].NumberOfComment,
                VoteDiff = mapRecipe[redipeId].VoteDiff,
                Vote = mapRecipe[redipeId].Vote,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt
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
