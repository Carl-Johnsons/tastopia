using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.Comments;
public class GetRecipeCommentsCommand : IRequest<Result<PaginatedRecipeCommentListResponse?>>
{
    public Guid? RecipeId { get; init; }
    public int? Skip { get; init; }
}

public class GetRecipeCommentsCommandHandler : IRequestHandler<GetRecipeCommentsCommand, Result<PaginatedRecipeCommentListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Comment, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IMapper _mapper;
    public GetRecipeCommentsCommandHandler(IApplicationDbContext context,
        IPaginateDataUtility<Comment, AdvancePaginatedMetadata> paginateDataUtility,
        GrpcUser.GrpcUserClient grpcUserClient,
        IMapper mapper)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedRecipeCommentListResponse?>> Handle(GetRecipeCommentsCommand request, CancellationToken cancellationToken)
    {
        var recipeId = request.RecipeId;
        var skip = request.Skip;

        if (recipeId == null || skip == null)
        {
            return Result<PaginatedRecipeCommentListResponse?>.Failure(RecipeError.NotFound);
        }

        var recipe = _context.Recipes.Where(r => r.Id == recipeId).FirstOrDefault();

        if (recipe == null || recipe.Comments.Count == 0)
        {
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
        var recipeComments = recipe.Comments;

        var totalPage = (recipeComments.Count + RECIPE_CONSTANTS.COMMENT_LIMIT - 1) / RECIPE_CONSTANTS.COMMENT_LIMIT;

        recipeComments = recipeComments.Skip((skip ?? 0) * RECIPE_CONSTANTS.COMMENT_LIMIT).Take(RECIPE_CONSTANTS.COMMENT_LIMIT).ToList();


        var comments = recipeComments.Select(c => new RecipeCommentResponse
        {
            Id = c.Id,
            AccountId = c.AccountId,
            Content = c.Content,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
            IsActive = c.IsActive,
            RecipeId = c.Id,
        }).ToList();

        if (comments == null || comments.Count == 0)
        {
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

        var accountIds = recipeComments
            .Select(r => r.AccountId.ToString())
            .Distinct()
            .ToList();

        var response = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { _mapper.Map<RepeatedField<string>>(accountIds) }
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

        foreach (var comment in comments)
        {
            comment.AvatarUrl = mapUsers[comment.AccountId].AvtUrl;
            comment.DisplayName = mapUsers[comment.AccountId].DisplayName;
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
