using Contract.Event.UploadEvent.EventModel;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("UploadMultipleImageFileEvent")]
public record UploadMultipleImageFileEvent
{
    public List<FileStreamEvent> FileStreamEvents { get; set; } = null!;
}
