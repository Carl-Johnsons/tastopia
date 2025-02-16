using Contract.Constants;
using MassTransit;

namespace Contract.Event.NotificationEvent;

[EntityName("PushNotificationEvent")]
public class NotifyUserEvent
{
    public List<ActorDTO> PrimaryActors { get; set; } = [];
    public List<ActorDTO> SecondaryActors { get; set; } = [];
    public NotificationTemplateCode TemplateCode { get; set; }
    public List<string> Channels { get; set; } = [];
    public string? JsonData { get; set; }
    public string? ImageUrl { get; set; }
}

public class ActorDTO
{
    public Guid ActorId { get; set; }
    public EntityType Type { get; set; }
}
