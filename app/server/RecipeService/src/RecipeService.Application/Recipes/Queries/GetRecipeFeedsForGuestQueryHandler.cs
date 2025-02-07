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

public class GetRecipeFeedsForGuestQuery : IRequest<Result<PaginatedRecipeFeedsListResponse?>>
{
    [Required]
    public List<string>? TagValues { get; init; }
}

public class GetRecipeFeedsForGuestQueryHandler : IRequestHandler<GetRecipeFeedsForGuestQuery, Result<PaginatedRecipeFeedsListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IMapper _mapper;

    public GetRecipeFeedsForGuestQueryHandler(IApplicationDbContext context, IPaginateDataUtility<Recipe, AdvancePaginatedMetadata> paginateDataUtility, GrpcUserClient grpcUserClient, IMapper mapper)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedRecipeFeedsListResponse?>> Handle(GetRecipeFeedsForGuestQuery request, CancellationToken cancellationToken)
    {
        var tagValues = request.TagValues;
        var skip = 0;

        if (tagValues == null || !tagValues.Any())
        {
            return Result<PaginatedRecipeFeedsListResponse>.Failure(RecipeError.NotFound);
        }

        tagValues.RemoveAll(string.IsNullOrEmpty);

        if (!tagValues.Any())
        {
            return Result<PaginatedRecipeFeedsListResponse>.Failure(RecipeError.NotFound);
        }


        var recipesQuery = _context.Recipes.Where(r => r.IsActive == true).OrderByDescending(r => r.CreatedAt).AsQueryable();


        if (!tagValues.Contains("All"))
        {
            recipesQuery = recipesQuery.Where(r => tagValues.Any(tag =>
                r.Title.ToLower().Contains(tag.ToLower()) ||
                r.Description.ToLower().Contains(tag.ToLower()) ||
                r.Ingredients.Any(ingredient => ingredient.ToLower().Contains(tag.ToLower()))
            ));
        }

        var totalPage = (await recipesQuery.CountAsync() + RECIPE_CONSTANTS.RECIPE_LIMIT - 1) / RECIPE_CONSTANTS.RECIPE_LIMIT;


        recipesQuery = _paginateDataUtility.PaginateQuery(recipesQuery, new PaginateParam
        {
            Offset = skip * RECIPE_CONSTANTS.RECIPE_LIMIT,
            Limit = RECIPE_CONSTANTS.RECIPE_LIMIT
        });




        var recipeList = await recipesQuery.Select(r =>
        new RecipeFeedResponse
        {
            AuthorAvtUrl = "",
            AuthorDisplayName = "",
            AuthorId = r.AuthorId,
            RecipeImgUrl = r.ImageUrl,
            Description = r.Description,
            Id = r.Id,
            Title = r.Title,
            NumberOfComment = r.NumberOfComment,
            VoteDiff = r.VoteDiff,
            Vote = Vote.None.ToString(),
        }).Take(5).ToListAsync();

        if (recipeList == null || !recipeList.Any())
        {
            return Result<PaginatedRecipeFeedsListResponse?>.Success(new PaginatedRecipeFeedsListResponse
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
            return Result<PaginatedRecipeFeedsListResponse>.Failure(RecipeError.NotFound);
        }

        foreach (var recipe in recipeList)
        {
            recipe.AuthorDisplayName = mapUsers[recipe.AuthorId].DisplayName;
            recipe.AuthorAvtUrl = mapUsers[recipe.AuthorId].AvtUrl;
        }

        var paginatedResponse = new PaginatedRecipeFeedsListResponse
        {
            PaginatedData = recipeList,
            Metadata = new AdvancePaginatedMetadata
            {
                TotalPage = totalPage,
                HasNextPage = false,
            }
        };
        return Result<PaginatedRecipeFeedsListResponse?>.Success(paginatedResponse);
    }
}
