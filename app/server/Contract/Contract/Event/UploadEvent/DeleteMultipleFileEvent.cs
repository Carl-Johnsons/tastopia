using Contract.Event.UploadEvent.EventModel;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("delete-multiple-file-event")]
public record DeleteMultipleFileEvent
{
    public List<Guid?> FileIds { get; set; } = null!;
}
