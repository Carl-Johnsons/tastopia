using Contract.Event.UploadEvent.EventModel;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("UpdateMultipleImageFileEvent")]
public record UpdateMultipleImageFileEvent
{
    public List<FileStreamEvent>? FileStreamEvents { get; set; } 
    public List<string>? DeleteUrls { get; set; }
}
