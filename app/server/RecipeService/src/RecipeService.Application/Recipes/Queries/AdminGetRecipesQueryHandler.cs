using AutoMapper;
using Contract.DTOs;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UserProto;
namespace RecipeService.Application.Recipes.Queries;
public class AdminGetRecipesQuery : IRequest<Result<PaginatedAdminRecipeListResponse?>>
{
    public Guid AccountId { get; set; }
    public PaginatedDTO paginatedDTO { get; set; } = null!;
}
public class AdminGetRecipesQueryHandler : IRequestHandler<AdminGetRecipesQuery, Result<PaginatedAdminRecipeListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<AdminRecipeResponse, NumberedPaginatedMetadata> _paginateDataUtility;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    public AdminGetRecipesQueryHandler(IApplicationDbContext context, IPaginateDataUtility<AdminRecipeResponse, NumberedPaginatedMetadata> paginateDataUtility, IMapper mapper, GrpcUser.GrpcUserClient grpcUserClient)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _mapper = mapper;
        _grpcUserClient = grpcUserClient;
    }
    public async Task<Result<PaginatedAdminRecipeListResponse?>> Handle(AdminGetRecipesQuery request, CancellationToken cancellationToken)
    {
        var skip = request.paginatedDTO.Skip;
        var keyword = request.paginatedDTO.Keyword;
        var accountId = request.AccountId;
        if(accountId == Guid.Empty || skip == null || skip < 0)
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

        var recipesQuery = _context.Recipes.AsQueryable();
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

        var recipes = await recipesQuery
            .Select(r => new
            {
                Recipe = new AdminRecipeResponse
                {
                    Id = r.Id,
                    AuthorId = r.AuthorId,
                    Title = r.Title,
                    Ingredients = "",
                    RecipeImageUrl = r.ImageUrl,
                    CreatedAt = r.CreatedAt,
                    IsActive = r.IsActive,
                    AuthorDisplayName = "",
                    AuthorUsername = ""
                },
                Ingredients = r.Ingredients
            })
            .ToListAsync();

        if (recipes == null || recipes.Count == 0)
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
        var authorIds = recipes
        .Select(r => r.Recipe.AuthorId)
        .Distinct()
        .ToHashSet();
        var response = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { _mapper.Map<RepeatedField<string>>(authorIds) }
        }, cancellationToken: cancellationToken);

        if (response == null || response.Users.Count != authorIds.Count)
        {
            return Result<PaginatedAdminRecipeListResponse?>.Failure(RecipeError.NotFound);
        }

        foreach (var r in recipes)
        {
            r.Recipe.AuthorDisplayName = response.Users[r.Recipe.AuthorId.ToString()].DisplayName;
            r.Recipe.AuthorUsername = response.Users[r.Recipe.AuthorId.ToString()].AccountUsername;
            r.Recipe.Ingredients = string.Join(", ", r.Ingredients);
        }

        var recipesResponseQuery = recipes.Select(r => r.Recipe).AsQueryable();

        var limit = RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT;
        if(request.paginatedDTO.Limit != null)
        {
            limit = request.paginatedDTO.Limit.Value;
        }
        var totalRow = recipesResponseQuery.Count();
        var totalPage = (totalRow + limit - 1) / limit;
        recipesResponseQuery = _paginateDataUtility.PaginateQuery(recipesResponseQuery, new PaginateParam
        {
            Offset = (skip ?? 0) * limit,
            Limit = limit,
            SortBy = request.paginatedDTO.SortBy != null ? request.paginatedDTO.SortBy : "CreatedAt",
            SortOrder = request.paginatedDTO.SortOrder != null ? request.paginatedDTO.SortOrder : SortType.DESC,
        });
        var result = recipesResponseQuery.ToList();
        if (result == null || result.Count == 0)
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
        var paginatedResponse = new PaginatedAdminRecipeListResponse
        {
            PaginatedData = result,
            Metadata = new NumberedPaginatedMetadata
            {
                TotalPage = totalPage,
                CurrentPage = (skip ?? 0) + 1,
                TotalRow = totalRow,
            }
        };
        return Result<PaginatedAdminRecipeListResponse?>.Success(paginatedResponse);
    }
}
