using AutoMapper;
using Contract.Constants;
using Contract.DTOs;
using Contract.Utilities;
using Google.Protobuf.Collections;
using RecipeProto;
using TrackingService.Domain.Responses;
using UserProto;

namespace TrackingService.Application.ActivityLog.Queries;

public record GetAdminActivityLogQuery : IRequest<Result<PaginatedAdminActivityLogListResponse>>
{
    public string Lang { get; set; } = "en";
    public PaginatedDTO DTO { get; init; } = null!;
    public Guid AccountId { get; init; }
}


public class GetAdminActivityLogQueryHandler : IRequestHandler<GetAdminActivityLogQuery, Result<PaginatedAdminActivityLogListResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaginateDataUtility<AdminActivityLogResponse, NumberedPaginatedMetadata> _paginateDataUtility;
    private readonly IMapper _mapper;
    private readonly GrpcRecipe.GrpcRecipeClient _grpcRecipeClient;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    public GetAdminActivityLogQueryHandler(IApplicationDbContext context,
                                           IPaginateDataUtility<AdminActivityLogResponse, NumberedPaginatedMetadata> paginateDataUtility,
                                           IMapper mapper,
                                           GrpcRecipe.GrpcRecipeClient grpcRecipeClient,
                                           GrpcUser.GrpcUserClient grpcUserClient)
    {
        _context = context;
        _paginateDataUtility = paginateDataUtility;
        _mapper = mapper;
        _grpcRecipeClient = grpcRecipeClient;
        _grpcUserClient = grpcUserClient;
    }

    public async Task<Result<PaginatedAdminActivityLogListResponse>> Handle(GetAdminActivityLogQuery request,
                                                                      CancellationToken cancellationToken)
    {
        var normalizedLanguage = LanguageUtility.ToIso6391(request.Lang);
        var limit = request.DTO.Limit ?? ENTITY_LIMIT.ADMIN_ACTIVITY_LOG;
        var offset = (request.DTO.Skip * limit ?? 0);

        var query = _context.AdminActivityLogs
            .Where(aal => aal.AccountId == request.AccountId)
            .Select(aal => new AdminActivityLogResponse
            {
                AccountId = aal.AccountId,
                ActivityType = aal.ActivityType,
                EntityId = aal.EntityId,
                EntityType = aal.EntityType,
                SecondaryEntityId = aal.SecondaryEntityId,
                SecondaryEntityType = aal.SecondaryEntityType,
                CreatedAt = aal.CreatedAt,
                UpdatedAt = aal.UpdatedAt
            });

        var totalRow = query.Count();
        var totalPage = (totalRow + limit - 1) / limit;

        var paginatedQuery = _paginateDataUtility.PaginateQuery(query, new PaginateParam
        {
            Limit = limit,
            Offset = offset,
            SortBy = "CreatedAt",
            SortOrder = SortType.DESC
        });

        if (paginatedQuery.Count() == 0)
        {
            return Result<PaginatedAdminActivityLogListResponse>.Success(new PaginatedAdminActivityLogListResponse
            {
                PaginatedData = [],
                Metadata = new NumberedPaginatedMetadata
                {
                    CurrentPage = (request.DTO.Skip ?? 0) + 1,
                    TotalPage = totalPage,
                    TotalRow = totalRow
                }
            });
        }
        // Get recipe detail dictionary
        var recipeIdList = paginatedQuery
            .ToList()
            .Where(aal => aal.EntityType == ActivityEntityType.RECIPE || (aal.SecondaryEntityId != null && aal.SecondaryEntityType == ActivityEntityType.RECIPE))
            .Select(aal => (Guid)(aal.EntityType == ActivityEntityType.RECIPE ? aal.EntityId : aal.SecondaryEntityId!))
            .ToList();

        GrpcMapSimpleRecipes? grpcRecipeMap = null;

        if (recipeIdList.Count > 0)
        {
            var repeatedField = _mapper.Map<RepeatedField<string>>(recipeIdList);

            grpcRecipeMap = await _grpcRecipeClient.GetSimpleRecipesAsync(new GrpcGetSimpleRecipeRequest
            {
                AccountId = request.AccountId.ToString(),
                RecipeIds = { repeatedField }
            }, cancellationToken: cancellationToken);
        }
        // Get comment detail dictionary

        GrpcMapSimpleComments? grpcCommentMap = null;
        var commentAndRecipeIdList = paginatedQuery
            .ToList()
            .Where(aal => aal.EntityType == ActivityEntityType.COMMENT && aal.SecondaryEntityType == ActivityEntityType.RECIPE)
            .Select(aal => (aal.SecondaryEntityId + "~" + aal.EntityId))
            .ToList();

        if (commentAndRecipeIdList.Count > 0)
        {
            var repeatedField = _mapper.Map<RepeatedField<string>>(commentAndRecipeIdList);

            grpcCommentMap = await _grpcRecipeClient.GetSimpleCommentsAsync(new GrpcGetSimpleCommentRequest
            {
                Ids = { repeatedField }
            }, cancellationToken: cancellationToken);
        }

        // Get user report dictionary

        GrpcSimpleUserReport? grpcUserReportMap = null;
        var userReportIdList = paginatedQuery
            .ToList()
            .Where(aal => aal.EntityType == ActivityEntityType.REPORT_USER)
            .Select(aal => aal.EntityId)
            .ToList();

        if (userReportIdList.Count > 0)
        {
            var repeatedField = _mapper.Map<RepeatedField<string>>(userReportIdList);

            grpcUserReportMap = await _grpcUserClient.GetSimpleUserReportAsync(new GrpcGetUserReportRequest
            {
                ReportIds = { repeatedField },
                Lang = normalizedLanguage
            }, cancellationToken: cancellationToken);
        }

        // Get comment report dictionary
        GrpcMapCommentReports? grpcCommentReportMap = null;
        var commentReportIdList = paginatedQuery
            .ToList()
            .Where(aal => aal.EntityType == ActivityEntityType.REPORT_COMMENT)
            .Select(aal => aal.EntityId)
            .ToList();

        if (commentReportIdList.Count > 0)
        {
            var repeatedField = _mapper.Map<RepeatedField<string>>(commentReportIdList);

            grpcCommentReportMap = await _grpcRecipeClient.GetCommentReportsAsync(new GrpcGetCommentReportRequest
            {
                Ids = { repeatedField },
                Lang = normalizedLanguage
            }, cancellationToken: cancellationToken);
        }

        // Get comment report dictionary

        GrpcMapRecipeReports? grpcRecipeReportMap = null;
        var recipeReportIdList = paginatedQuery
            .ToList()
            .Where(aal => aal.EntityType == ActivityEntityType.REPORT_RECIPE)
            .Select(aal => aal.EntityId)
            .ToList();

        if (recipeReportIdList.Count > 0)
        {
            var repeatedField = _mapper.Map<RepeatedField<string>>(recipeReportIdList);

            grpcRecipeReportMap = await _grpcRecipeClient.GetRecipeReportsAsync(new GrpcGetRecipeReportRequest
            {
                Ids = { repeatedField },
                Lang = normalizedLanguage
            }, cancellationToken: cancellationToken);
        }

        // Get user dictionary

        GrpcGetSimpleUsersDTO? grpcUserMap = null;
        var accountIdList = paginatedQuery
            .ToList()
            .Where(aal => aal.EntityType == ActivityEntityType.USER)
            .Select(aal => aal.EntityId)
            .ToList();

        if (accountIdList.Count > 0)
        {
            var repeatedField = _mapper.Map<RepeatedField<string>>(accountIdList);

            grpcUserMap = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
            {
                AccountId = { repeatedField },
            }, cancellationToken: cancellationToken);
        }

        // Get tags dictionary
        GrpcMapTagResponse? grpcTagMap = null;
        var tagIdList = paginatedQuery
            .ToList()
            .Where(aal => aal.EntityType == ActivityEntityType.TAG)
            .Select(aal => aal.EntityId)
            .ToList();

        if (tagIdList.Count > 0)
        {
            var repeatedField = _mapper.Map<RepeatedField<string>>(tagIdList);

            grpcTagMap = await _grpcRecipeClient.GetTagsAsync(new GrpcGetTagsRequest
            {
                Ids = { repeatedField },
            }, cancellationToken: cancellationToken);
        }
        var list = paginatedQuery.ToList()
                                 .Select(aal =>
                                 {
                                     if (aal.EntityType == ActivityEntityType.RECIPE && grpcRecipeMap != null)
                                     {
                                         var grpcSimpleRecipe = grpcRecipeMap.Recipes[aal.EntityId.ToString()];

                                         var mapEntity = new RecipeAdminActivityLogResponse
                                         {
                                             AccountId = aal.AccountId,
                                             ActivityType = aal.ActivityType,
                                             EntityId = aal.EntityId,
                                             EntityType = aal.EntityType,
                                             SecondaryEntityId = aal.SecondaryEntityId,
                                             SecondaryEntityType = aal.SecondaryEntityType,
                                             CreatedAt = aal.CreatedAt,
                                             UpdatedAt = aal.UpdatedAt,
                                         };
                                         var recipeLogResponse = new RecipeLogResponse
                                         {
                                             Id = aal.EntityId,
                                             Title = grpcSimpleRecipe.Title,
                                             AuthorId = Guid.Parse(grpcSimpleRecipe.AuthorId),
                                             AuthorDisplayName = grpcSimpleRecipe.AuthorDisplayName,
                                             AuthorUsername = grpcSimpleRecipe.AuthorUsername,
                                             ImageURL = grpcSimpleRecipe.RecipeImgUrl,
                                             CreatedAt = grpcSimpleRecipe.CreatedAt.ToDateTime(),
                                             UpdatedAt = grpcSimpleRecipe.UpdatedAt.ToDateTime(),
                                             VoteDiff = grpcSimpleRecipe.VoteDiff
                                         };

                                         mapEntity.Recipe = recipeLogResponse;

                                         return mapEntity;
                                     }

                                     if (aal.EntityType == ActivityEntityType.COMMENT && grpcRecipeMap != null && grpcCommentMap != null)
                                     {
                                         var grpcSimpleRecipe = grpcRecipeMap.Recipes[aal.SecondaryEntityId.ToString()];
                                         var grpcSimpleComment = grpcCommentMap.Comments[aal.SecondaryEntityId.ToString() + "~" + aal.EntityId.ToString()];

                                         var mapEntity = new CommentAdminActivityLogResponse
                                         {
                                             AccountId = aal.AccountId,
                                             ActivityType = aal.ActivityType,
                                             EntityId = aal.EntityId,
                                             EntityType = aal.EntityType,
                                             SecondaryEntityId = aal.SecondaryEntityId,
                                             SecondaryEntityType = aal.SecondaryEntityType,
                                             CreatedAt = aal.CreatedAt,
                                             UpdatedAt = aal.UpdatedAt,
                                         };

                                         var recipeLogResponse = new RecipeLogResponse
                                         {
                                             Id = (Guid)aal.SecondaryEntityId!,
                                             Title = grpcSimpleRecipe.Title,
                                             AuthorId = Guid.Parse(grpcSimpleRecipe.AuthorId),
                                             AuthorDisplayName = grpcSimpleRecipe.AuthorDisplayName,
                                             AuthorUsername = grpcSimpleRecipe.AuthorUsername,
                                             ImageURL = grpcSimpleRecipe.RecipeImgUrl,
                                             CreatedAt = grpcSimpleRecipe.CreatedAt.ToDateTime(),
                                             UpdatedAt = grpcSimpleRecipe.UpdatedAt.ToDateTime(),
                                             VoteDiff = grpcSimpleRecipe.VoteDiff
                                         };

                                         var commentLogResponse = new CommentLogResponse
                                         {
                                             Id = aal.EntityId,
                                             RecipeId = (Guid)aal.SecondaryEntityId!,
                                             AuthorAvatarURL = grpcSimpleComment.AuthorAvatarURL,
                                             AuthorDisplayName = grpcSimpleComment.AuthorDisplayName,
                                             AuthorId = Guid.Parse(grpcSimpleComment.AuthorId),
                                             AuthorUsername = grpcSimpleComment.AuthorUsername,
                                             Content = grpcSimpleComment.Content,
                                             CreatedAt = grpcSimpleComment.CreatedAt.ToDateTime(),
                                             UpdatedAt = grpcSimpleComment.UpdatedAt.ToDateTime(),
                                             IsActive = grpcSimpleComment.IsActive
                                         };

                                         mapEntity.Recipe = recipeLogResponse;
                                         mapEntity.Comment = commentLogResponse;

                                         return mapEntity;
                                     }

                                     if (aal.EntityType == ActivityEntityType.USER && grpcUserMap != null)
                                     {
                                         var grpcUser = grpcUserMap.Users[aal.EntityId.ToString()];

                                         var mapEntity = new UserAdminActivityLogResponse
                                         {
                                             AccountId = aal.AccountId,
                                             ActivityType = aal.ActivityType,
                                             EntityId = aal.EntityId,
                                             EntityType = aal.EntityType,
                                             SecondaryEntityId = aal.SecondaryEntityId,
                                             SecondaryEntityType = aal.SecondaryEntityType,
                                             CreatedAt = aal.CreatedAt,
                                             UpdatedAt = aal.UpdatedAt,
                                         };

                                         var user = new UserLogResponse
                                         {
                                             Id = aal.EntityId,
                                             AvatarURL = grpcUser.AvtUrl,
                                             DisplayName = grpcUser.DisplayName,
                                             Username = grpcUser.AccountUsername
                                         };

                                         mapEntity.User = user;

                                         return mapEntity;
                                     }

                                     if (aal.EntityType == ActivityEntityType.REPORT_USER && grpcUserReportMap != null)
                                     {
                                         var grpcUserReport = grpcUserReportMap.Reports[aal.EntityId.ToString()];

                                         var mapEntity = new UserReportAdminActivityLogResponse
                                         {
                                             AccountId = aal.AccountId,
                                             ActivityType = aal.ActivityType,
                                             EntityId = aal.EntityId,
                                             EntityType = aal.EntityType,
                                             SecondaryEntityId = aal.SecondaryEntityId,
                                             SecondaryEntityType = aal.SecondaryEntityType,
                                             CreatedAt = aal.CreatedAt,
                                             UpdatedAt = aal.UpdatedAt,
                                         };

                                         var report = new ReportLogResponse
                                         {
                                             AdditionalDetail = grpcUserReport.Report.AdditionalDetail,
                                             CreatedAt = grpcUserReport.Report.CreatedAt.ToDateTime(),
                                             Id = aal.EntityId,
                                             Reasons = grpcUserReport.Report.Reasons.ToList(),
                                             ReporterAccountId = Guid.Parse(grpcUserReport.Report.ReporterAccountId),
                                             Status = grpcUserReport.Report.Status
                                         };

                                         var user = new UserLogResponse
                                         {
                                             Id = Guid.Parse(grpcUserReport.User.AccountId),
                                             AvatarURL = grpcUserReport.User.AvtUrl,
                                             DisplayName = grpcUserReport.User.DisplayName,
                                             Username = grpcUserReport.User.AccountUsername
                                         };

                                         var reporter = new UserLogResponse
                                         {
                                             Id = Guid.Parse(grpcUserReport.Reporter.AccountId),
                                             AvatarURL = grpcUserReport.Reporter.AvtUrl,
                                             DisplayName = grpcUserReport.Reporter.DisplayName,
                                             Username = grpcUserReport.Reporter.AccountUsername
                                         };
                                         mapEntity.Report = report;
                                         mapEntity.User = user;
                                         mapEntity.Reporter = reporter;

                                         return mapEntity;
                                     }

                                     if (aal.EntityType == ActivityEntityType.REPORT_COMMENT && grpcCommentReportMap != null)
                                     {
                                         var grpcCommentReport = grpcCommentReportMap.CommentReports[aal.EntityId.ToString()];

                                         var mapEntity = new CommentReportAdminActivityLogResponse
                                         {
                                             AccountId = aal.AccountId,
                                             ActivityType = aal.ActivityType,
                                             EntityId = aal.EntityId,
                                             EntityType = aal.EntityType,
                                             SecondaryEntityId = aal.SecondaryEntityId,
                                             SecondaryEntityType = aal.SecondaryEntityType,
                                             CreatedAt = aal.CreatedAt,
                                             UpdatedAt = aal.UpdatedAt,
                                         };

                                         var report = new ReportLogResponse
                                         {
                                             Id = aal.EntityId,
                                             AdditionalDetail = grpcCommentReport.Report.AdditionalDetail,
                                             ReporterAccountId = Guid.Parse(grpcCommentReport.Report.ReporterAccountId),
                                             CreatedAt = grpcCommentReport.Report.CreatedAt.ToDateTime(),
                                             Reasons = grpcCommentReport.Report.Reasons.ToList(),
                                             Status = grpcCommentReport.Report.Status
                                         };

                                         var recipeLogResponse = new RecipeLogResponse
                                         {
                                             Id = Guid.Parse(grpcCommentReport.Recipe.Id),
                                             Title = grpcCommentReport.Recipe.Title,
                                             AuthorId = Guid.Parse(grpcCommentReport.Recipe.AuthorId),
                                             AuthorDisplayName = grpcCommentReport.Recipe.AuthorDisplayName,
                                             AuthorUsername = grpcCommentReport.Recipe.AuthorUsername,
                                             ImageURL = grpcCommentReport.Recipe.RecipeImgUrl,
                                             CreatedAt = grpcCommentReport.Recipe.CreatedAt.ToDateTime(),
                                             UpdatedAt = grpcCommentReport.Recipe.UpdatedAt.ToDateTime(),
                                             VoteDiff = grpcCommentReport.Recipe.VoteDiff
                                         };

                                         var commentLogResponse = new CommentLogResponse
                                         {
                                             Id = Guid.Parse(grpcCommentReport.Comment.Id),
                                             RecipeId = Guid.Parse(grpcCommentReport.Recipe.Id),
                                             AuthorAvatarURL = grpcCommentReport.Comment.AuthorAvatarURL,
                                             AuthorDisplayName = grpcCommentReport.Comment.AuthorDisplayName,
                                             AuthorId = Guid.Parse(grpcCommentReport.Comment.AuthorId),
                                             AuthorUsername = grpcCommentReport.Comment.AuthorUsername,
                                             Content = grpcCommentReport.Comment.Content,
                                             CreatedAt = grpcCommentReport.Comment.CreatedAt.ToDateTime(),
                                             UpdatedAt = grpcCommentReport.Comment.UpdatedAt.ToDateTime(),
                                             IsActive = grpcCommentReport.Comment.IsActive
                                         };

                                         var reporter = new UserLogResponse
                                         {
                                             Id = Guid.Parse(grpcCommentReport.Reporter.AccountId),
                                             AvatarURL = grpcCommentReport.Reporter.AvtUrl,
                                             DisplayName = grpcCommentReport.Reporter.DisplayName,
                                             Username = grpcCommentReport.Reporter.AccountUsername
                                         };

                                         mapEntity.Recipe = recipeLogResponse;
                                         mapEntity.Comment = commentLogResponse;
                                         mapEntity.Report = report;
                                         mapEntity.Reporter = reporter;

                                         return mapEntity;
                                     }

                                     if (aal.EntityType == ActivityEntityType.REPORT_RECIPE && grpcRecipeReportMap != null)
                                     {
                                         var grpcRecipeReport = grpcRecipeReportMap.RecipeReports[aal.EntityId.ToString()];

                                         var mapEntity = new RecipeReportAdminActivityLogResponse
                                         {
                                             AccountId = aal.AccountId,
                                             ActivityType = aal.ActivityType,
                                             EntityId = aal.EntityId,
                                             EntityType = aal.EntityType,
                                             SecondaryEntityId = aal.SecondaryEntityId,
                                             SecondaryEntityType = aal.SecondaryEntityType,
                                             CreatedAt = aal.CreatedAt,
                                             UpdatedAt = aal.UpdatedAt,
                                         };

                                         var report = new ReportLogResponse
                                         {
                                             Id = aal.EntityId,
                                             AdditionalDetail = grpcRecipeReport.Report.AdditionalDetail,
                                             ReporterAccountId = Guid.Parse(grpcRecipeReport.Report.ReporterAccountId),
                                             CreatedAt = grpcRecipeReport.Report.CreatedAt.ToDateTime(),
                                             Reasons = grpcRecipeReport.Report.Reasons.ToList(),
                                             Status = grpcRecipeReport.Report.Status
                                         };

                                         var recipeLogResponse = new RecipeLogResponse
                                         {
                                             Id = Guid.Parse(grpcRecipeReport.Recipe.Id),
                                             Title = grpcRecipeReport.Recipe.Title,
                                             AuthorId = Guid.Parse(grpcRecipeReport.Recipe.AuthorId),
                                             AuthorDisplayName = grpcRecipeReport.Recipe.AuthorDisplayName,
                                             AuthorUsername = grpcRecipeReport.Recipe.AuthorUsername,
                                             ImageURL = grpcRecipeReport.Recipe.RecipeImgUrl,
                                             CreatedAt = grpcRecipeReport.Recipe.CreatedAt.ToDateTime(),
                                             UpdatedAt = grpcRecipeReport.Recipe.UpdatedAt.ToDateTime(),
                                             VoteDiff = grpcRecipeReport.Recipe.VoteDiff
                                         };

                                         var reporter = new UserLogResponse
                                         {
                                             AvatarURL = grpcRecipeReport.Reporter.AvtUrl,
                                             DisplayName = grpcRecipeReport.Reporter.DisplayName,
                                             Id = Guid.Parse(grpcRecipeReport.Reporter.AccountId),
                                             Username = grpcRecipeReport.Reporter.AccountUsername
                                         };

                                         mapEntity.Recipe = recipeLogResponse;
                                         mapEntity.Report = report;
                                         mapEntity.Reporter = reporter;

                                         return mapEntity;
                                     }

                                     if (aal.EntityType == ActivityEntityType.TAG && grpcTagMap != null)
                                     {
                                         var grpcTag = grpcTagMap.Tags[aal.EntityId.ToString()];

                                         var mapEntity = new TagAdminActivityLogResponse
                                         {
                                             AccountId = aal.AccountId,
                                             ActivityType = aal.ActivityType,
                                             EntityId = aal.EntityId,
                                             EntityType = aal.EntityType,
                                             SecondaryEntityId = aal.SecondaryEntityId,
                                             SecondaryEntityType = aal.SecondaryEntityType,
                                             CreatedAt = aal.CreatedAt,
                                             UpdatedAt = aal.UpdatedAt,
                                         };

                                         var tag = new TagLogResponse
                                         {
                                             Id = aal.EntityId,
                                             Code = grpcTag.Code,
                                             Category = grpcTag.Category,
                                             ImageUrl = grpcTag.ImageUrl,
                                             Status = grpcTag.Status,
                                             Value = normalizedLanguage == LanguageValidation.En ? grpcTag.En : grpcTag.Vi,
                                         };
                                         mapEntity.Tag = tag;
                                         return mapEntity;
                                     }

                                     return aal;
                                 });

        var paginatedResponse = new PaginatedAdminActivityLogListResponse
        {
            PaginatedData = list,
            Metadata = new NumberedPaginatedMetadata
            {
                CurrentPage = (request.DTO.Skip ?? 0) + 1,
                TotalPage = totalPage,
                TotalRow = totalRow
            }
        };

        return Result<PaginatedAdminActivityLogListResponse>.Success(paginatedResponse);
    }
}
