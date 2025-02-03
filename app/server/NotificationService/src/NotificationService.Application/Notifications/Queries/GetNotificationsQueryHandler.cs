
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NotificationService.Application.Responses;
using System.Text.RegularExpressions;
using UserProto;

namespace NotificationService.Application.Notifications.Queries;

public record GetNotificationsQuery : IRequest<Result<List<NotificationsResponse>>>
{
    public Guid AccountId { get; set; }
    public string Language { get; init; } = "en";
}

public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, Result<List<NotificationsResponse>>>
{
    private readonly IApplicationDbContext _context;
    private readonly GrpcUser.GrpcUserClient _userClient;

    public GetNotificationsQueryHandler(IApplicationDbContext context, GrpcUser.GrpcUserClient userClient)
    {
        _context = context;
        _userClient = userClient;
    }

    public Task<Result<List<NotificationsResponse>>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var database = _context.GetDatabase();
        var notificationsCollection = database.GetCollection<Notification>("Notification");

        var pipeline = new[]
        {
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "NotificationTemplate" },
                { "localField", "TemplateId" },
                { "foreignField", "_id" },
                { "as", "Template" }
            }),
            new BsonDocument("$unwind", "$Template")
        };

        var result = notificationsCollection.Aggregate<BsonDocument>(pipeline).ToList();

        foreach (var doc in result)
        {
            Console.WriteLine(doc.ToJson());
        }

        var responses = result.Select(r => BsonSerializer.Deserialize<Notification>(r))
                                .Select(n =>
                                {
                                    var paIds = (n?.PrimaryActors ?? []).Select(pa => pa.ActorId).ToList();
                                    var saIds = (n?.SecondaryActors ?? []).Select(pa => pa.ActorId).ToList();

                                    var message = n?.Template?.TranslationMessages.GetValueOrDefault(request.Language)
                                                  ?? "Fallback message";
                                    var title = n?.Template?.TranslationTitles?.GetValueOrDefault(request.Language)
                                                ?? "Fallback title";

                                    message = SafeFormat(message, string.Join(", ", paIds), string.Join(", ", saIds));
                                    title = SafeFormat(title, string.Join(", ", paIds), string.Join(", ", saIds));

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

        return Task.FromResult(Result<List<NotificationsResponse>>.Success(responses));
    }

    private string SafeFormat(string template, params object[] args)
    {
        if (string.IsNullOrEmpty(template))
            return template;

        // Regular expression to find placeholders in the format {n}
        var regex = new Regex(@"\{(\d+)\}");
        return regex.Replace(template, match =>
        {
            // Extract the index from the placeholder
            if (int.TryParse(match.Groups[1].Value, out int index))
            {
                // Check if the index is within the bounds of the arguments array
                if (index >= 0 && index < args.Length)
                {
                    // Return the argument's string representation
                    return args[index]?.ToString() ?? string.Empty;
                }
            }
            // If no corresponding argument is found, return an empty string or a default value
            return string.Empty; // Or use a default value like "[Missing]"
        });
    }
}
