using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeProto;
using RecipeService.Domain.Responses;
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


    public GetUserViewRecipeDetaiQueryHandler(GrpcRecipe.GrpcRecipeClient grpcRecipeClient, IMapper mapper, IApplicationDbContext context, IPaginateDataUtility<UserViewRecipeDetail, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _grpcRecipeClient = grpcRecipeClient;
        _mapper = mapper;
        _context = context;
        _paginateDataUtility = paginateDataUtility;
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

        await Console.Out.WriteLineAsync("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa:"+skip);

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
            return Result<PaginatedUserViewRecipeDetailListResponse?>.Failure(UserViewRecipeDetailError.NotFound, "Not found simple recipes");
        }

        var mapRecipe = response.Recipes.ToDictionary(r => r.Key);
        var mapRecipe1 = response.Recipes;

        var listView = new List<UserViewRecipeDetailResponse>();

        foreach (var v in views)
        {
            var redipeId = v.RecipeId.ToString();
            var view = new UserViewRecipeDetailResponse
            {
                Id = Guid.Parse(mapRecipe1[redipeId].Id),
                AuthorId = Guid.Parse(mapRecipe1[redipeId].AuthorId),
                Title = mapRecipe1[redipeId].Title,
                Description = mapRecipe1[redipeId].Description,
                RecipeImgUrl = mapRecipe1[redipeId].RecipeImgUrl,
                AuthorAvtUrl = mapRecipe1[redipeId].AuthorAvtUrl,
                AuthorDisplayName = mapRecipe1[redipeId].AuthorDisplayName,
                NumberOfComment = mapRecipe1[redipeId].NumberOfComment,
                VoteDiff = mapRecipe1[redipeId].VoteDiff,
                Vote = mapRecipe1[redipeId].Vote,
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
