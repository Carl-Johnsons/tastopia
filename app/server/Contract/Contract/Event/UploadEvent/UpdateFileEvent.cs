using Contract.Event.UploadEvent.EventModel;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("UpdateFileEvent")]
public record UpdateFileEvent
{
    public string Url { get; set; } = null!;
    public FileStreamEvent FileStreamEvent { get; set; } = null!;
}
