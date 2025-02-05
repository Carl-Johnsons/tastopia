using AutoMapper;
using Contract.DTOs.UserDTO;
using Google.Protobuf.Collections;
using MongoDB.Driver;
using NotificationService.Domain.Constants;
using NotificationService.Domain.Errors;
using NotificationService.Domain.Responses;
using SmartFormat;
using UserProto;

namespace NotificationService.Application.Notifications.Queries;

public record GetNotificationsQuery : IRequest<Result<PaginatedNotificationListResponse?>>
{
    public Guid AccountId { get; set; }
    public string Language { get; init; } = "en";
    public int? Skip { get; init; } = 0;
}

public partial class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, Result<PaginatedNotificationListResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly IMapper _mapper;
    private readonly IPaginateDataUtility<Notification, AdvancePaginatedMetadata> _paginateDataUtility;

    public GetNotificationsQueryHandler(IApplicationDbContext context,
                                        GrpcUser.GrpcUserClient grpcUserClient,
                                        IMapper mapper,
                                        IPaginateDataUtility<Notification, AdvancePaginatedMetadata> paginateDataUtility)
    {
        _context = context;
        _grpcUserClient = grpcUserClient;
        _mapper = mapper;
        _paginateDataUtility = paginateDataUtility;
    }

    public async Task<Result<PaginatedNotificationListResponse?>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
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
                    PrimaryActors = n.PrimaryActors,
                    SecondaryActors = n.SecondaryActors,
                    TemplateId = n.TemplateId,
                    CreatedAt = n.CreatedAt,
                    ImageUrl = n.ImageUrl,
                    JsonData = n.JsonData,
                    UpdatedAt = n.UpdatedAt,
                    Template = nt
                }
            );

        var totalPage = (notificationQuery.Count() / NOTIFICATION_CONSTANT.NOTIFICATION_LIMIT) + 1;

        var paginatedNotificationQuery = _paginateDataUtility.PaginateQuery(notificationQuery, new PaginateParam
        {
            Offset = request.Skip * NOTIFICATION_CONSTANT.NOTIFICATION_LIMIT,
            Limit = NOTIFICATION_CONSTANT.NOTIFICATION_LIMIT
        });

        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(paginatedNotificationQuery.ToList(), Newtonsoft.Json.Formatting.Indented));

        var actorIdSets = paginatedNotificationQuery.SelectMany(n => n.PrimaryActors.Concat(n.SecondaryActors)
                                                                  .Select(merge => merge.ActorId.ToString()))
                                  .ToHashSet();

        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(actorIdSets, Newtonsoft.Json.Formatting.Indented));
        var res = await _grpcUserClient.GetSimpleUserAsync(new GrpcGetSimpleUsersRequest
        {
            AccountId = { _mapper.Map<RepeatedField<string>>(actorIdSets) }
        }, cancellationToken: cancellationToken);

        var mapUsers = new Dictionary<Guid, SimpleUser>();
        foreach (var (key, value) in res.Users)
        {
            mapUsers[Guid.Parse(key)] = new SimpleUser
            {
                AccountId = Guid.Parse(value.AccountId),
                AvtUrl = value.AvtUrl,
                DisplayName = value.DisplayName,
            };
        }
        if (res == null || mapUsers.Count != actorIdSets.Count)
        {
            return Result<PaginatedNotificationListResponse?>.Failure(NotificationErrors.NotFound, "Actor not found");
        }
        var responses = paginatedNotificationQuery
                                .ToList()
                                .Select(n =>
                                {
                                    if (n == null)
                                    {
                                        return null;
                                    }
                                    var paNames = n.PrimaryActors.Select(pa => mapUsers[pa.ActorId].DisplayName).ToList();
                                    var saNames = n.SecondaryActors.Select(sa => mapUsers[sa.ActorId].DisplayName).ToList();

                                    var message = n.Template?.TranslationMessages.GetValueOrDefault(request.Language)
                                                  ?? "";
                                    var title = n.Template?.TranslationTitles?.GetValueOrDefault(request.Language)
                                                ?? "";

                                    var data = new
                                    {
                                        Actors = paNames,
                                        Targets = saNames,
                                        IsSelf = false
                                    };

                                    message = Smart.Format(message, data);
                                    title = Smart.Format(title);

                                    return new NotificationsResponse
                                    {
                                        Id = n.Id,
                                        CreatedAt = n.CreatedAt,
                                        UpdatedAt = n.UpdatedAt,
                                        ImageUrl = n.ImageUrl,
                                        JsonData = n.JsonData,
                                        Message = message,
                                        Title = title
                                    };
                                })
                                .ToList();

        var paginatedResponse = new PaginatedNotificationListResponse
        {
            PaginatedData = responses!,
            Metadata = new AdvancePaginatedMetadata
            {
                HasNextPage = request.Skip >= totalPage,
                TotalPage = totalPage
            }
        };

        return Result<PaginatedNotificationListResponse?>.Success(paginatedResponse);
    }

}
