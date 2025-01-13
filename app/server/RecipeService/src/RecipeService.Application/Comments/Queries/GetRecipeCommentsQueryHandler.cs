using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.Comments.Queries;
public class GetRecipeCommentsQuery : IRequest<Result<PaginatedRecipeCommentListResponse?>>
{
    public Guid? RecipeId { get; init; }
    public int? Skip { get; init; }
}

public class GetRecipeCommentsQueryHandler : IRequestHandler<GetRecipeCommentsQuery, Result<PaginatedRecipeCommentListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Comment, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IMapper _mapper;

    public GetRecipeCommentsQueryHandler(IApplicationDbContext context, IPaginateDataUtility<Comment, AdvancePaginatedMetadata> paginateDataUtility, GrpcUser.GrpcUserClient grpcUserClient, IMapper mapper)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedRecipeCommentListResponse?>> Handle(GetRecipeCommentsQuery request, CancellationToken cancellationToken)
    {
        var recipeId = request.RecipeId;
        var skip = request.Skip;

        if (recipeId == null || skip == null)
        {
            return Result<PaginatedRecipeCommentListResponse?>.Failure(RecipeError.NotFound);
        }

        var commentQuery = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable()
                            .Where(r => r.Id == recipeId).SelectMany(r => r.Comments).OrderByDescending(c => c.CreatedAt).AsQueryable();


        var totalPage = (commentQuery.Count() + RECIPE_CONSTANTS.COMMENT_LIMIT - 1) / RECIPE_CONSTANTS.COMMENT_LIMIT;

        commentQuery = _paginateDataUtility.PaginateQuery(commentQuery, new PaginateParam
        {
            Offset = (skip ?? 0) * RECIPE_CONSTANTS.COMMENT_LIMIT,
            Limit = RECIPE_CONSTANTS.COMMENT_LIMIT
        });


        var comments = commentQuery.Select(c => new RecipeCommentResponse
        {
            Id = c.Id,
            AccountId = c.AccountId,
            Content = c.Content,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt,
            IsActive = c.IsActive,
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

        var accountIds = commentQuery
        .Select(r => r.AccountId.ToString())
        .Distinct()
        .ToHashSet();

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
