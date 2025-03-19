using AutoMapper;
using Contract.Constants;
using Contract.DTOs;
using Contract.Utilities;
using Google.Protobuf.Collections;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UserProto;

namespace RecipeService.Application.Reports.Queries;

public record GetCommentReportsQuery : IRequest<Result<PaginatedAdminReportCommentListResponse>>
{
    public string Lang { get; init; } = null!;
    public PaginatedDTO PaginatedDTO { get; init; } = null!;
}

public class GetCommentReportsQueryHandler : IRequestHandler<GetCommentReportsQuery, Result<PaginatedAdminReportCommentListResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IPaginateDataUtility<AdminReportCommentResponse, NumberedPaginatedMetadata> _paginateDataUtility;

    public GetCommentReportsQueryHandler(IApplicationDbContext context,
                                         IMapper mapper,
                                         GrpcUser.GrpcUserClient grpcUserClient,
                                         IPaginateDataUtility<AdminReportCommentResponse, NumberedPaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _mapper = mapper;
        _grpcUserClient = grpcUserClient;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedAdminReportCommentListResponse>> Handle(GetCommentReportsQuery request,
                                                                        CancellationToken cancellationToken)
    {
        var keyword = request.PaginatedDTO?.Keyword;
        var limit = request.PaginatedDTO?.Limit ?? RECIPE_CONSTANTS.ADMIN_RECIPE_LIMIT;
        var normalizedLangue = LanguageUtility.ToIso6391(request.Lang);

        var userReportCommentCollection = _context.GetDatabase().GetCollection<UserReportComment>(nameof(UserReportComment));
        var recipeCollection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe));

        var pipeline = userReportCommentCollection.Aggregate()
            .Lookup<UserReportComment, Recipe, UserReportCommentWithRecipe>(foreignCollection: recipeCollection,
                                                                            localField: urc => urc.RecipeId,
                                                                            foreignField: r => r.Id,
                                                                            @as: urcwr => urcwr.Recipe)
            .Unwind<UserReportCommentWithRecipe, UserReportCommentWithRecipe>(urcwr => urcwr.Recipe)
            .Project<UserReportCommentWithRecipe>(
                    Builders<UserReportCommentWithRecipe>.Projection
                    .Exclude(urcwr => urcwr.Recipe.RecipeVotes)
                    .Exclude(urcwr => urcwr.Recipe.RecipeTags)
                    .Exclude(urcwr => urcwr.Recipe.Steps)
            )
            .Project(Builders<UserReportCommentWithRecipe>.Projection.Expression(report => new UserReportCommentWithRecipe
            {
                Id = report.Id,
                EntityId = report.EntityId,
                RecipeId = report.RecipeId,
                Recipe = report.Recipe,
                Comment = report.Recipe.Comments.FirstOrDefault(c => c.Id == report.EntityId)!,
                AdditionalDetails = report.AdditionalDetails,
                AccountId = report.AccountId,
                ReasonCodes = report.ReasonCodes,
                Status = report.Status,
                CreatedAt = report.CreatedAt,
                UpdatedAt = report.UpdatedAt
            }))
            .Project<UserReportCommentWithRecipe>(
                Builders<UserReportCommentWithRecipe>.Projection
                    .Exclude(urcwr => urcwr.Recipe.Comments)
            );
        ;

        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            var searchUserResponse = await _grpcUserClient.SearchUserAsync(new GrpcSearchUserRequest
            {
                Keyword = keyword,
            }, cancellationToken: cancellationToken);


            var searchAuthorIds = searchUserResponse.AccountIds.Select(Guid.Parse).ToHashSet();
            var searchRecipeReportReasonCode = ReportReasonData.CommentReportReasons.Where(rrr => normalizedLangue
                                                                                                 == LanguageValidation.Vi ? rrr.Vi.ToLower().Contains(keyword) : rrr.En.ToLower().Contains(keyword))
                                                                                   .Select(rrr => rrr.Code)
                                                                                   .ToList();
            // Escape the keyword to handle special regex characters like *
            var escapedKeyword = Regex.Escape(keyword);
            var titleFilter = Builders<UserReportCommentWithRecipe>.Filter.Regex(urwr => urwr.Recipe!.Title, new BsonRegularExpression(escapedKeyword, "i"));
            var contentFilter = Builders<UserReportCommentWithRecipe>.Filter.Regex(urwr => urwr.Comment.Content, new BsonRegularExpression(escapedKeyword, "i"));
            var reasonFilter = Builders<UserReportCommentWithRecipe>.Filter.In("ReasonCodes", searchRecipeReportReasonCode);
            var authorIdFilter = Builders<UserReportCommentWithRecipe>.Filter.In(urwr => urwr.Comment.AccountId, searchAuthorIds);
            var reporterIdFilter = Builders<UserReportCommentWithRecipe>.Filter.In(urwr => urwr.AccountId, searchAuthorIds);

            var combinedFilter = Builders<UserReportCommentWithRecipe>.Filter.Or(titleFilter,
                                                                                 contentFilter,
                                                                                 reasonFilter,
                                                                                 authorIdFilter,
                                                                                 reporterIdFilter);

            pipeline = pipeline.Match(combinedFilter);
        }


        var userReportList = await pipeline.ToListAsync();

        if (userReportList == null || userReportList.Count == 0)
        {
            return Result<PaginatedAdminReportCommentListResponse>.Success(new PaginatedAdminReportCommentListResponse
            {
                PaginatedData = [],
                Metadata = new NumberedPaginatedMetadata
                {
                    CurrentPage = (request.PaginatedDTO?.Skip ?? 0) + 1,
                    TotalPage = 0
                }
            });
        }
        var accountId = userReportList.Select(ur => ur.AccountId).Concat(userReportList.Select(ur => ur.Comment!.AccountId)).ToList();
        var repeatedField = _mapper.Map<RepeatedField<string>>(accountId);

        var mapGrpcUserResponse = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { repeatedField },
        }, cancellationToken: cancellationToken);

        var totalRow = userReportList.Count();
        var totalPage = (totalRow + limit - 1) / limit;

        var adminReportCommentResponse = userReportList.Select(ur => new AdminReportCommentResponse
        {
            CommentId = ur.Comment.Id,
            CommentContent = ur.Comment.Content,
            CommentOwnerUsername = mapGrpcUserResponse.Users[ur.Comment.AccountId.ToString()].AccountUsername,
            CreatedAt = ur.CreatedAt,
            RecipeImageURL = ur.Recipe.ImageUrl,
            RecipeTitle = ur.Recipe.Title,
            ReporterUsername = mapGrpcUserResponse.Users[ur.AccountId.ToString()].AccountUsername,
            ReportId = ur.Id,
            Status = ur.Status.ToString(),
            ReportReason = string.Join(", ", ReportReasonData.CommentReportReasons.Where(rrr => ur.ReasonCodes.Contains(rrr.Code))
                                                                                  .Select(rrr => normalizedLangue == LanguageValidation.Vi ? rrr.Vi : rrr.En)
                                                                                  .ToList()),
        }).AsQueryable();

        var paginatedQuery = _paginateDataUtility.PaginateQuery(adminReportCommentResponse, new PaginateParam
        {
            Limit = limit,
            Offset = (request.PaginatedDTO?.Skip ?? 0) * limit,
            SortBy = request.PaginatedDTO?.SortBy ?? "CreatedAt",
            SortOrder = request.PaginatedDTO?.SortOrder ?? SortType.DESC
        });

        var paginatedResult = paginatedQuery.ToList();

        var paginatedReportComments = new PaginatedAdminReportCommentListResponse
        {
            PaginatedData = paginatedResult,
            Metadata = new NumberedPaginatedMetadata
            {
                CurrentPage = (request.PaginatedDTO?.Skip ?? 0) + 1,
                TotalPage = totalPage,
                TotalRow = totalRow
            },
        };

        return Result<PaginatedAdminReportCommentListResponse>.Success(paginatedReportComments);
    }

    private class UserReportCommentWithRecipe : UserReportComment
    {
        public Recipe Recipe { get; set; } = null!;
        public Comment Comment { get; set; } = null!;
    }
}
