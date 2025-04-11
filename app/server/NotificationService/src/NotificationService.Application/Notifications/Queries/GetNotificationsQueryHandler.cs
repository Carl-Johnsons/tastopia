using AutoMapper;
using Contract.Constants;
using Google.Protobuf.Collections;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using NotificationService.Domain.Constants;
using NotificationService.Domain.Responses;
using RecipeProto;
using SmartFormat;
using UserProto;

namespace NotificationService.Application.Notifications.Queries;

public record GetNotificationsQuery : IRequest<Result<PaginatedNotificationListResponse?>>
{
    public Guid AccountId { get; set; }
    public string Language { get; init; } = "en";
    public int? Skip { get; init; }
    public NotificationCategories Category { get; init; }
}

public partial class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, Result<PaginatedNotificationListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly GrpcRecipe.GrpcRecipeClient _grpcRecipeClient;
    private readonly IMapper _mapper;
    private readonly IPaginateDataUtility<Notification, AdvancePaginatedMetadata> _paginateDataUtility;
    private readonly ILogger<GetNotificationsQueryHandler> _logger;

    public GetNotificationsQueryHandler(IApplicationDbContext context,
                                        GrpcUser.GrpcUserClient grpcUserClient,
                                        IMapper mapper,
                                        IPaginateDataUtility<Notification, AdvancePaginatedMetadata> paginateDataUtility,
                                        ILogger<GetNotificationsQueryHandler> logger,
                                        GrpcRecipe.GrpcRecipeClient grpcRecipeClient)
    {
        _context = context;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
        _paginateDataUtility = paginateDataUtility;
        _logger = logger;
        _grpcRecipeClient = grpcRecipeClient;
    }

    public async Task<Result<PaginatedNotificationListResponse?>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var MAX_NAME = 20;
        var skip = request.Skip ?? 0;
        var database = _context.GetDatabase();
        var nQuery = database.GetCollection<Notification>(nameof(Notification))
                             .AsQueryable()
                             .AsQueryable();
        var ntQuery = database.GetCollection<NotificationTemplate>(nameof(NotificationTemplate))
                              .AsQueryable()
                              .AsQueryable();

        var notificationQuery = nQuery.Join(
                inner: ntQuery,
                outerKeySelector: n => n.TemplateId,
                innerKeySelector: nt => nt.Id,
                resultSelector: (n, nt) => new Notification
                {
                    Id = n.Id,
                    Recipients = n.Recipients,
                    PrimaryActors = n.PrimaryActors,
                    SecondaryActors = n.SecondaryActors,
                    TemplateId = n.TemplateId,
                    CreatedAt = n.CreatedAt,
                    ImageUrl = n.ImageUrl,
                    JsonData = n.JsonData,
                    UpdatedAt = n.UpdatedAt,
                    Template = nt
                }
            ).Where(n => n.Recipients.Any(sa => sa.RecipientId == request.AccountId))
            .OrderByDescending(n => n.CreatedAt)
            .AsQueryable();

        List<string> userNotificationCategory = [
            NotificationTemplateCode.USER_UPVOTE.ToString(),
            NotificationTemplateCode.USER_FOLLOW.ToString(),
            NotificationTemplateCode.USER_COMMENT.ToString(),
            NotificationTemplateCode.USER_CREATE_RECIPE.ToString()
        ];

        if (request.Category == NotificationCategories.USER)
        {
            notificationQuery = notificationQuery.Where(n => userNotificationCategory.Contains(n.Template!.TemplateCode.ToString()));
        }
        else if (request.Category == NotificationCategories.SYSTEM)
        {
            notificationQuery = notificationQuery.Where(n => !userNotificationCategory.Contains(n.Template!.TemplateCode.ToString()));
        }

        var unReadNotifications = notificationQuery.Where(n => n.Recipients.Any(sa => sa.RecipientId == request.AccountId && !sa.IsViewed)).Count();
        var totalPage = (notificationQuery.Count() + NOTIFICATION_CONSTANT.NOTIFICATION_LIMIT - 1) / NOTIFICATION_CONSTANT.NOTIFICATION_LIMIT;
        var paginatedNotificationQuery = _paginateDataUtility.PaginateQuery(notificationQuery, new PaginateParam
        {
            Offset = skip * NOTIFICATION_CONSTANT.NOTIFICATION_LIMIT,
            Limit = NOTIFICATION_CONSTANT.NOTIFICATION_LIMIT
        });
        List<NotificationsResponse?> responses = [];

        if (paginatedNotificationQuery.Count() > 0)
        {
            // Get user detail map
            GrpcGetSimpleUsersDTO? grpcUserMap = null;

            var userIdList = paginatedNotificationQuery
                .Select(n => new
                {
                    PrimaryUserIds = n.PrimaryActors.Where(a => a.Type.ToString() == EntityType.USER.ToString()).Select(a => a.ActorId).ToList(),
                    SecondaryUserIds = n.SecondaryActors.Where(a => a.Type.ToString() == EntityType.USER.ToString()).Select(a => a.ActorId).ToList()
                })
                .SelectMany(x => x.PrimaryUserIds.Concat(x.SecondaryUserIds))
                .Distinct()
                .ToList();

            var actorIdSet = paginatedNotificationQuery.SelectMany(n => n.PrimaryActors.Concat(n.SecondaryActors)).ToList();

            Console.WriteLine(JsonConvert.SerializeObject(actorIdSet, Formatting.Indented));
            Console.WriteLine(JsonConvert.SerializeObject(userIdList, Formatting.Indented));

            if (userIdList.Count > 0)
            {
                var repeatedField = _mapper.Map<RepeatedField<string>>(userIdList);
                grpcUserMap = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
                {
                    AccountId = { _mapper.Map<RepeatedField<string>>(repeatedField) }
                }, cancellationToken: cancellationToken);
            }

            var recipeIdList = paginatedNotificationQuery
                .Select(n => new
                {
                    PrimaryRecipeIds = n.PrimaryActors.Where(a => a.Type.ToString() == EntityType.RECIPE.ToString()).Select(a => a.ActorId).ToList(),
                    SecondaryRecipeIds = n.SecondaryActors.Where(a => a.Type.ToString() == EntityType.RECIPE.ToString()).Select(a => a.ActorId).ToList()
                })
                .SelectMany(x => x.PrimaryRecipeIds.Concat(x.SecondaryRecipeIds))
                .Distinct()
                .ToList();

            // Get recipe detail map
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

            Console.WriteLine(JsonConvert.SerializeObject(recipeIdList, Formatting.Indented));

            // Get comment detail map
            GrpcMapSimpleComments? grpcCommentMap = null;
            var commentAndRecipeIdList = paginatedNotificationQuery
                .Select(n => new
                {
                    PrimaryRecipeAndCommentIds = n.PrimaryActors.Where(a => a.Type.ToString() == EntityType.COMMENT.ToString()).Select(a => a.ActorId).ToList(),
                    SecondaryRecipeAndCommentIds = n.SecondaryActors.Where(a => a.Type.ToString() == EntityType.COMMENT.ToString()).Select(a => a.ActorId).ToList()
                })
                .SelectMany(x => x.PrimaryRecipeAndCommentIds.Concat(x.SecondaryRecipeAndCommentIds))
                .Distinct()
                .ToList();

            if (commentAndRecipeIdList.Count > 0)
            {
                var repeatedField = _mapper.Map<RepeatedField<string>>(commentAndRecipeIdList);

                grpcCommentMap = await _grpcRecipeClient.GetSimpleCommentsAsync(new GrpcGetSimpleCommentRequest
                {
                    Ids = { repeatedField }
                }, cancellationToken: cancellationToken);
            }

            responses = paginatedNotificationQuery
                                   .ToList()
                                   .Select(n =>
                                   {
                                       if (n == null)
                                       {
                                           return null;
                                       }
                                       var paNames = n.PrimaryActors.Select(pa =>
                                       {
                                           var finalName = "";
                                           switch (pa.Type)
                                           {
                                               case EntityType.USER:
                                                   finalName = grpcUserMap?.Users[pa.ActorId.ToString()].DisplayName ?? "";
                                                   break;
                                               case EntityType.RECIPE:
                                                   finalName = grpcRecipeMap?.Recipes[pa.ActorId.ToString()].Title ?? "";
                                                   break;
                                               case EntityType.COMMENT:
                                                   finalName = grpcCommentMap?.Comments[pa.ActorId].Content ?? "";
                                                   break;
                                           }

                                           return finalName.Length > MAX_NAME ? finalName.Substring(0, MAX_NAME) + "..." : finalName;
                                       }).ToList();

                                       var saNames = n.SecondaryActors.Select(sa =>
                                       {
                                           var finalName = "";
                                           switch (sa.Type)
                                           {
                                               case EntityType.USER:
                                                   finalName = grpcUserMap?.Users[sa.ActorId.ToString()].DisplayName ?? "";
                                                   break;
                                               case EntityType.RECIPE:
                                                   finalName = grpcRecipeMap?.Recipes[sa.ActorId.ToString()].Title ?? "";
                                                   break;
                                               case EntityType.COMMENT:
                                                   finalName = grpcCommentMap?.Comments[sa.ActorId].Content ?? "";
                                                   break;
                                           }

                                           return finalName.Length > MAX_NAME ? finalName.Substring(0, MAX_NAME) + "..." : finalName;

                                       }).ToList();

                                       var message = n.Template?.TranslationMessages.GetValueOrDefault(request.Language)
                                                     ?? "";
                                       var title = n.Template?.TranslationTitles?.GetValueOrDefault(request.Language)
                                                   ?? "";

                                       var data = new
                                       {
                                           Actors = paNames,
                                           Targets = saNames,
                                           IsSelf = true
                                       };

                                       var IsViewed = n.Recipients.Where(r => r.RecipientId == request.AccountId).Select(r => r.IsViewed).SingleOrDefault();

                                       message = Smart.Format(message, data);
                                       title = Smart.Format(title);

                                       return new NotificationsResponse
                                       {
                                           Id = n.Id,
                                           IsViewed = IsViewed,
                                           CreatedAt = n.CreatedAt,
                                           UpdatedAt = n.UpdatedAt,
                                           ImageUrl = n.ImageUrl,
                                           JsonData = n.JsonData,
                                           Code = n.Template?.TemplateCode.ToString() ?? "General",
                                           Message = message,
                                           Title = title
                                       };
                                   })
                                   .ToList();
        }

        var paginatedResponse = new PaginatedNotificationListResponse
        {
            PaginatedData = responses!,
            Metadata = new NotificationListMetadata
            {
                HasNextPage = skip < totalPage - 1,
                TotalPage = totalPage,
                UnreadNotifications = unReadNotifications
            }
        };
        _logger.LogInformation(JsonConvert.SerializeObject(paginatedResponse, Formatting.Indented));
        return Result<PaginatedNotificationListResponse?>.Success(paginatedResponse);
    }

}
