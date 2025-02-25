using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UserProto;
namespace RecipeService.Application.Recipes.Queries;
public class AdminGetRecipesQuery : IRequest<Result<PaginatedAdminRecipeListResponse?>>
{
    public int? Page { get; set; }
    public string? Keyword { get; set; } 
    public Guid AccountId { get; set; }
}
public class AdminGetRecipesQueryHandler : IRequestHandler<AdminGetRecipesQuery, Result<PaginatedAdminRecipeListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Recipe, NumberedPaginatedMetadata> _paginateDataUtility;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    public AdminGetRecipesQueryHandler(IApplicationDbContext context, IPaginateDataUtility<Recipe, NumberedPaginatedMetadata> paginateDataUtility, IMapper mapper, GrpcUser.GrpcUserClient grpcUserClient)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _mapper = mapper;
        _grpcUserClient = grpcUserClient;
    }
    public async Task<Result<PaginatedAdminRecipeListResponse?>> Handle(AdminGetRecipesQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page;
        var keyword = request.Keyword;
        var accountId = request.AccountId;
        if(accountId == Guid.Empty || page == null || page < 0)
        {
            return Result<PaginatedAdminRecipeListResponse?>.Failure(RecipeError.NullParameter, "AccountId or Page is null");
        }

        var adminResponse = await _grpcUserClient.GetUserDetailAsync(new GrpcAccountIdRequest
        {
            AccountId = accountId.ToString(),
        }, cancellationToken: cancellationToken);

        if(adminResponse == null || !adminResponse.IsAdmin)
        {
            return Result<PaginatedAdminRecipeListResponse?>.Failure(RecipeError.PermissionDeny);
        }

        var recipesQuery = _context.Recipes.OrderByDescending(r => r.CreatedAt).AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
        {
            var searchUserResponse = await _grpcUserClient.SearchUserAsync(new GrpcSearchUserRequest
            {
                Keyword = keyword,
            }, cancellationToken: cancellationToken);

            var searchAuthorIds = searchUserResponse.AccountIds.ToHashSet();

            keyword = keyword.ToLower();
            recipesQuery = recipesQuery.Where(r =>
                r.Title.ToLower().Contains(keyword) ||
                r.Description.ToLower().Contains(keyword) ||
                r.Ingredients.Any(ingredient => ingredient.ToLower().Contains(keyword)) ||
                r.Steps.Any(step => step.Content.ToLower().Contains(keyword)) ||
                searchAuthorIds.Contains(r.AuthorId.ToString())
            );
        }
        var totalPage = (await recipesQuery.CountAsync() + RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT - 1) / RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT;
        recipesQuery = _paginateDataUtility.PaginateQuery(recipesQuery, new PaginateParam
        {
            Offset = (page ?? 0) * RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT,
            Limit = RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT
        });
        var recipes = await recipesQuery.ToListAsync();
        var recipeList = recipes.Select(r =>
        new AdminRecipeResponse
        {
            Id = r.Id,
            AuthorId = r.AuthorId,
            Title = r.Title,
            Ingredients = string.Join(", ", r.Ingredients),
            RecipeImageUrl = r.ImageUrl,
            CreatedAt = r.CreatedAt,
            IsActive = r.IsActive,
            AuthorDisplayName = "",
            AuthorUsername = ""
        });
        if (recipeList == null || !recipeList.Any())
        {
            return Result<PaginatedAdminRecipeListResponse?>.Success(new PaginatedAdminRecipeListResponse
            {
                PaginatedData = [],
                Metadata = new NumberedPaginatedMetadata
                {
                    CurrentPage = 0,
                    TotalPage = 0,
                }
            });
        }
        var authorIds = recipesQuery
        .Select(r => r.AuthorId)
        .Distinct()
        .ToHashSet();
        var response = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { _mapper.Map<RepeatedField<string>>(authorIds) }
        }, cancellationToken: cancellationToken);

        var mapUsers = new Dictionary<Guid, SimpleUser>();
        foreach (var (key, value) in response.Users)
        {
            mapUsers[Guid.Parse(key)] = new SimpleUser
            {
                AccountId = Guid.Parse(value.AccountId),
                AvtUrl = value.AvtUrl,
                DisplayName = value.DisplayName,
                AccountUsername = value.AccountUsername,
            };
        }
        if (response == null || mapUsers.Count != authorIds.Count)
        {
            return Result<PaginatedAdminRecipeListResponse?>.Failure(RecipeError.NotFound);
        }
        foreach (var recipe in recipeList)
        {
            recipe.AuthorDisplayName = mapUsers[recipe.AuthorId].DisplayName;
            recipe.AuthorUsername = mapUsers[recipe.AuthorId].AccountUsername;
        }
        var paginatedResponse = new PaginatedAdminRecipeListResponse
        {
            PaginatedData = recipeList,
            Metadata = new NumberedPaginatedMetadata
            {
                TotalPage = totalPage,
                CurrentPage = page!.Value
            }
        };
        return Result<PaginatedAdminRecipeListResponse?>.Success(paginatedResponse);
    }
}
