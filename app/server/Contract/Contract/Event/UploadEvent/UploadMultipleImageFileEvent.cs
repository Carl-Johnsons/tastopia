using Contract.Event.UploadEvent.EventModel;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("upload-multiple-image-file-event")]
public record UploadMultipleImageFileEvent
{
    public List<FileStreamEvent> FileStreamEvents { get; set; } = null!;
}
