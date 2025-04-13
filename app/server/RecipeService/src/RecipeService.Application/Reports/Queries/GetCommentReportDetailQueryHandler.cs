
using AutoMapper;
using Contract.Constants;
using Contract.Utilities;
using Google.Protobuf.Collections;
using MongoDB.Driver;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UserProto;

namespace RecipeService.Application.Reports.Queries;
public record GetCommentReportDetailQuery : IRequest<Result<AdminReportCommentDetailResponse?>>
{
    public string Lang { get; init; } = null!;
    public Guid RecipeId { get; init; }
    public Guid CommentId { get; init; }
}

public class GetCommentReportDetailQueryHandler : IRequestHandler<GetCommentReportDetailQuery, Result<AdminReportCommentDetailResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;

    public GetCommentReportDetailQueryHandler(IApplicationDbContext context,
                                              GrpcUser.GrpcUserClient grpcUserClient,
                                              IMapper mapper)
    {
        _context = context;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
    }

    public async Task<Result<AdminReportCommentDetailResponse?>> Handle(GetCommentReportDetailQuery request, CancellationToken cancellationToken)
    {
        var normalizedLangue = LanguageUtility.ToIso6391(request.Lang);

        var userReportCommentCollection = _context.GetDatabase().GetCollection<UserReportComment>(nameof(UserReportComment));
        var recipeCollection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe));

        var pipeline = recipeCollection.Aggregate()
            .Match(r => r.Id == request.RecipeId)
            .Lookup<Recipe, UserReportComment, RecipeWithReportList>(foreignCollection: userReportCommentCollection,
                                                                     localField: r => r.Id,
                                                                     foreignField: urc => urc.RecipeId,
                                                                     @as: rwrl => rwrl.UserReportComments)
            .Project(r => new RecipeWithReportList
            {
                Id = r.Id,
                MatchComment = r.Comments.FirstOrDefault(c => c.Id == request.CommentId)!,
                AuthorId = r.AuthorId,
                Ingredients = r.Ingredients,
                ImageUrl = r.ImageUrl,
                IsActive = r.IsActive,
                Description = r.Description,
                CookTime = r.CookTime,
                VoteDiff = r.VoteDiff,
                Steps = r.Steps,
                Serves = r.Serves,
                UserReportComments = r.UserReportComments,
                NumberOfComment = r.NumberOfComment,
                Title = r.Title,
                TotalView = r.TotalView,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt,
            })
            .SingleOrDefaultAsync(cancellationToken);

        var result = await pipeline;

        if (result == null)
        {
            return Result<AdminReportCommentDetailResponse>.Failure(ReportError.NotFound);
        }

        if (result.UserReportComments == null || result.UserReportComments.Count == 0)
        {
            return Result<AdminReportCommentDetailResponse>.Failure(ReportError.NotFound, "Recipe's comment is valid but there are no report associate with it.");
        }

        var accountIds = result.UserReportComments.Select(r => r.AccountId).ToList();
        accountIds.Add(result.AuthorId);
        accountIds.Add(result.MatchComment.AccountId);

        var repeatedField = _mapper.Map<RepeatedField<string>>(accountIds);
        var mapUserGrpc = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { repeatedField }
        }, cancellationToken: cancellationToken);

        var reportDetailResponse = new AdminReportCommentDetailResponse
        {
            Comment = new CommentDetailResponse
            {
                Id = result.MatchComment.Id,
                AuthorId = result.MatchComment.AccountId,
                AuthorUsername = mapUserGrpc.Users[result.MatchComment.AccountId.ToString()].AccountUsername,
                AuthorAvatarURL = mapUserGrpc.Users[result.MatchComment.AccountId.ToString()].AvtUrl,
                AuthorDisplayName = mapUserGrpc.Users[result.MatchComment.AccountId.ToString()].DisplayName,
                Content = result.MatchComment.Content,
                IsActive = result.MatchComment.IsActive,
                CreatedAt = result.MatchComment.CreatedAt,
                UpdatedAt = result.MatchComment.UpdatedAt,
            },
            Recipe = new AdminRecipeResponse
            {
                Id = result.Id,
                AuthorId = result.AuthorId,
                AuthorAvatarURL = mapUserGrpc.Users[result.AuthorId.ToString()].AvtUrl,
                AuthorDisplayName = mapUserGrpc.Users[result.AuthorId.ToString()].DisplayName,
                AuthorUsername = mapUserGrpc.Users[result.AuthorId.ToString()].AccountUsername,
                CreatedAt = result.CreatedAt,
                Description = result.Description,
                UpdatedAt = result.UpdatedAt,
                Ingredients = string.Join(", ", result.Ingredients),
                IsActive = result.IsActive,
                RecipeImageUrl = result.ImageUrl,
                Title = result.Title
            },
            Reports = result.UserReportComments.Select(urc => new ReportRecipeResponse
            {
                Id = urc.Id,
                AdditionalDetail = urc.AdditionalDetails,
                CreatedAt = result.CreatedAt,
                ReporterId = urc.AccountId,
                ReporterAvtUrl = mapUserGrpc.Users[urc.AccountId.ToString()].AvtUrl,
                ReporterDisplayName = mapUserGrpc.Users[urc.AccountId.ToString()].DisplayName,
                ReporterUsername = mapUserGrpc.Users[urc.AccountId.ToString()].AccountUsername,
                Status = urc.Status.ToString(),
                Reasons = ReportReasonData.CommentReportReasons.Where(rrr => urc.ReasonCodes.Contains(rrr.Code))
                                                              .Select(rrr => request.Lang == LanguageValidation.Vi ? rrr.Vi : rrr.En)
                                                              .ToList(),
            }).ToList()
        };
        return Result<AdminReportCommentDetailResponse?>.Success(reportDetailResponse);
    }

    private class RecipeWithReportList : Recipe
    {
        public Comment MatchComment { get; set; } = null!;
        public List<UserReportComment> UserReportComments { get; set; } = [];
    }
}
