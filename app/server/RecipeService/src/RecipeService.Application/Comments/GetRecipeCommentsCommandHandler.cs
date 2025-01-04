using Contract.DTOs.UserDTO;
using Contract.Event.UserEvent;
using Microsoft.EntityFrameworkCore;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;

namespace RecipeService.Application.Comments;
public class GetRecipeCommentsCommand : IRequest<Result<PaginatedRecipeCommentListResponse?>>
{
    public Guid? RecipeId { get; init; }
    public int? Skip { get; init; }
}

public class GetRecipeCommentsCommandHandler : IRequestHandler<GetRecipeCommentsCommand, Result<PaginatedRecipeCommentListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IServiceBus _serviceBus;
    private readonly IPaginateDataUtility<Comment, AdvancePaginatedMetadata> _paginateDataUtility;


    public GetRecipeCommentsCommandHandler(IServiceBus serviceBus, IApplicationDbContext context, IPaginateDataUtility<Comment, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _serviceBus = serviceBus;
        _context = context;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedRecipeCommentListResponse?>> Handle(GetRecipeCommentsCommand request, CancellationToken cancellationToken)
    {
        var recipeId = request.RecipeId;
        var skip = request.Skip;

        if (recipeId == null || skip == null) {
            return Result<PaginatedRecipeCommentListResponse?>.Failure(RecipeError.NotFound);
        }

        var commentsQuery = _context.Recipes.Where(r => r.Id == recipeId).SelectMany(r => r.Comments).OrderByDescending(c => c.CreatedAt).AsQueryable();

        var totalPage = (await commentsQuery.CountAsync() + RECIPE_CONSTANTS.COMMENT_LIMIT - 1) / RECIPE_CONSTANTS.COMMENT_LIMIT;


        commentsQuery = _paginateDataUtility.PaginateQuery(commentsQuery, new PaginateParam
        {
            Offset = (skip ?? 0) * RECIPE_CONSTANTS.COMMENT_LIMIT,
            Limit = RECIPE_CONSTANTS.COMMENT_LIMIT
        });

        var comments = await commentsQuery.Select(c => new RecipeCommentResponse
        {
            Id = c.Id,
            AccountId = c.AccountId,
            Content = c.Content,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
            IsActive = c.IsActive 
        }).ToListAsync();

        if(comments == null || comments.Count == 0) {
            return Result<PaginatedRecipeCommentListResponse?>.Success(new PaginatedRecipeCommentListResponse
            {
                PaginatedData = [],
                Metadata = new AdvancePaginatedMetadata
                {
                    HasNextPage = false,
                    TotalPage = 0
                }
            });
        }

        var accountIds = commentsQuery
        .Select(r => r.AccountId)
        .Distinct()
        .ToHashSet();

        var requestClient = _serviceBus.CreateRequestClient<GetSimpleUsersEvent>();

        var response = await requestClient.GetResponse<GetSimpleUsersDTO>(new GetSimpleUsersEvent
        {
            AccountIds = accountIds,
        });

        if (response == null || response.Message.Users.Count != accountIds.Count)
        {
            return Result<PaginatedRecipeCommentListResponse>.Failure(RecipeError.NotFound);
        }

        var mapUser = response.Message.Users;

        foreach(var comment in comments)
        {
            comment.AvatarUrl = mapUser[comment.AccountId].AvtUrl;
            comment.DisplayName = mapUser[comment.AccountId].DisplayName;
        }

        var hasNextPage = true;

        if (skip >= totalPage - 1)
        {
            hasNextPage = false;
        }

        var paginatedResponse = new PaginatedRecipeCommentListResponse
        {
            PaginatedData = comments,
            Metadata = new AdvancePaginatedMetadata
            {
                TotalPage = totalPage,
                HasNextPage = hasNextPage,
            }
        };
        return Result<PaginatedRecipeCommentListResponse?>.Success(paginatedResponse);
    }
}
