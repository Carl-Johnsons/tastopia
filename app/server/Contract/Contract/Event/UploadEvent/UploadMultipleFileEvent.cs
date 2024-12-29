using Contract.Event.UploadEvent.EventModel;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("UploadMultipleFileEvent")]
public record UploadMultipleFileEvent
{
    public List<FileStreamEvent> FileStreamEvents { get; set; } = null!;
}
