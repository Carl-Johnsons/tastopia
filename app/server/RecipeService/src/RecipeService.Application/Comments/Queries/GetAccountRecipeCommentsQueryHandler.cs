using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.Comments.Queries;
public class GetAccountRecipeCommentsQuery : IRequest<Result<PaginatedAccountRecipeCommentListResponse?>>
{
    public Guid? AccountId { get; init; }
    public int? Skip { get; init; }
}

public class GetAccountRecipeCommentsQueryHandler : IRequestHandler<GetAccountRecipeCommentsQuery, Result<PaginatedAccountRecipeCommentListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<Comment, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAccountRecipeCommentsQueryHandler> _logger;

    public GetAccountRecipeCommentsQueryHandler(IApplicationDbContext context, IPaginateDataUtility<Comment, AdvancePaginatedMetadata> paginateDataUtility, GrpcUser.GrpcUserClient grpcUserClient, IMapper mapper, ILogger<GetAccountRecipeCommentsQueryHandler> logger)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<PaginatedAccountRecipeCommentListResponse?>> Handle(GetAccountRecipeCommentsQuery request, CancellationToken cancellationToken)
    {
        var accountId = request.AccountId;
        var skip = request.Skip;

        if (accountId == null || skip == null)
        {
            return Result<PaginatedAccountRecipeCommentListResponse?>.Failure(RecipeError.NullParameter);
        }

        _logger.LogInformation("okkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk neeeeeeeeeee");

        var commentQuery = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable().SelectMany(r => r.Comments, (r, c) => new { RecipeTitle = r.Title, Comment = c }).Where(rc => rc.Comment.AccountId == accountId).OrderByDescending(rc => rc.Comment.CreatedAt).AsQueryable();
        var totalPage = (commentQuery.Count() + RECIPE_CONSTANTS.COMMENT_LIMIT - 1) / RECIPE_CONSTANTS.COMMENT_LIMIT;
        commentQuery = commentQuery.Skip(RECIPE_CONSTANTS.COMMENT_LIMIT * skip.Value).Take(RECIPE_CONSTANTS.COMMENT_LIMIT);

        var comments = commentQuery.Select(c => new AccountRecipeCommentResponse
        {
            Id = c.Comment.Id,
            AccountId = c.Comment.AccountId,
            Content = c.Comment.Content,
            CreatedAt = c.Comment.CreatedAt,
            UpdatedAt = c.Comment.UpdatedAt,
            IsActive = c.Comment.IsActive,
            RecipeTitle = c.RecipeTitle,
            DisplayName = "",
        }).ToList();

        if (comments == null || comments.Count == 0)
        {
            return Result<PaginatedAccountRecipeCommentListResponse?>.Success(new PaginatedAccountRecipeCommentListResponse
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
        .Select(r => r.Comment.AccountId.ToString())
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
            comment.DisplayName = mapUsers[comment.AccountId].DisplayName;
        }

        var hasNextPage = true;

        if (skip >= totalPage - 1)
        {
            hasNextPage = false;
        }

        var paginatedResponse = new PaginatedAccountRecipeCommentListResponse
        {
            PaginatedData = comments,
            Metadata = new AdvancePaginatedMetadata
            {
                TotalPage = totalPage,
                HasNextPage = hasNextPage,
            }
        };
        return Result<PaginatedAccountRecipeCommentListResponse?>.Success(paginatedResponse);
    }
}
