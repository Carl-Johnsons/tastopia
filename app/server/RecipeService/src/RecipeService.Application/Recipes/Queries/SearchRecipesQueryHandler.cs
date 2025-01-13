using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using System.ComponentModel.DataAnnotations;
using static UserProto.GrpcUser;
using UserProto;
using AutoMapper;

namespace RecipeService.Application.Recipes.Queries;

public class SearchRecipesQuery : IRequest<Result<PaginatedSearchRecipeListResponse?>>
{
    [Required]
    public int? Skip { get; init; }

    [Required]
    public List<string>? TagCodes { get; init; }

    [Required]
    public string? Keyword { get; init; }

    [Required]
    public Guid AccountId { get; init; }
}

public class SearchRecipesQueryHandler : IRequestHandler<SearchRecipesQuery, Result<PaginatedSearchRecipeListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IServiceBus _serviceBus;
    private readonly IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public SearchRecipesQueryHandler(IApplicationDbContext context, IServiceBus serviceBus, IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> paginateDataUtility, GrpcUserClient grpcUserClient, IMapper mapper)
    {
        _context = context;
        _serviceBus = serviceBus;
        _paginateDataUtility = paginateDataUtility;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedSearchRecipeListResponse?>> Handle(SearchRecipesQuery request, CancellationToken cancellationToken)
    {
        var skip = request.Skip;
        var tagCodes = request.TagCodes;
        var keyword = request.Keyword;

        if (skip == null)
        {
            return Result<PaginatedSearchRecipeListResponse?>.Failure(RecipeError.NotFound);
        }

        var recipesQuery = _context.Recipes.OrderByDescending(r => r.CreatedAt).AsQueryable();

        if (tagCodes != null && tagCodes.Any() && !tagCodes.Contains("ALL"))
        {
            var tagValues = _context.Tags.Where(t => tagCodes.Any(tc => tc == t.Code)).Select(t => t.Value).ToList();
            recipesQuery = recipesQuery.Where(r => tagValues.Any(tag =>
                r.Title.ToLower().Contains(tag.ToLower()) ||
                r.Description.ToLower().Contains(tag.ToLower()) ||
                r.Ingredients.Any(ingredient => ingredient.ToLower().Contains(tag.ToLower()))
            ));
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            recipesQuery = recipesQuery.Where(r =>
                r.Title.ToLower().Contains(keyword.ToLower()) ||
                r.Description.ToLower().Contains(keyword.ToLower()) ||
                r.Ingredients.Any(ingredient => ingredient.ToLower().Contains(keyword.ToLower()))
            );
        }

        var totalPage = (await recipesQuery.CountAsync() + RECIPE_CONSTANTS.RECIPE_LIMIT - 1) / RECIPE_CONSTANTS.RECIPE_LIMIT;


        recipesQuery = _paginateDataUtility.PaginateQuery(recipesQuery, new PaginateParam
        {
            Offset = (skip ?? 0) * RECIPE_CONSTANTS.RECIPE_LIMIT,
            Limit = RECIPE_CONSTANTS.RECIPE_LIMIT
        });

        var recipeList = await recipesQuery.Select(r =>
        new SearchRecipesResponse
        {
            AuthorAvtUrl = "",
            AuthorDisplayName = "",
            AuthorId = r.AuthorId,
            Description = r.Description,
            Id = r.Id,
            Title = r.Title,
            RecipeImageUrl = r.ImageUrl
        }).ToListAsync();

        if (recipeList == null || !recipeList.Any())
        {
            return Result<PaginatedSearchRecipeListResponse?>.Success(new PaginatedSearchRecipeListResponse
            {
                PaginatedData = [],
                Metadata = new AdvancePaginatedMetadata
                {
                    HasNextPage = false,
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
            };
        }

        if (response == null || mapUsers.Count != authorIds.Count)
        {
            return Result<PaginatedSearchRecipeListResponse?>.Failure(RecipeError.NotFound);
        }

        foreach (var recipe in recipeList)
        {
            recipe.AuthorDisplayName = mapUsers[recipe.AuthorId].DisplayName;
            recipe.AuthorAvtUrl = mapUsers[recipe.AuthorId].AvtUrl;
        }

        var hasNextPage = true;

        if (skip >= totalPage - 1)
        {
            hasNextPage = false;
        }

        var paginatedResponse = new PaginatedSearchRecipeListResponse
        {
            PaginatedData = recipeList,
            Metadata = new AdvancePaginatedMetadata
            {
                TotalPage = totalPage,
                HasNextPage = hasNextPage
            }
        };
        return Result<PaginatedSearchRecipeListResponse?>.Success(paginatedResponse);
    }
}
