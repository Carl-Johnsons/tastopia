using Contract.Event.UploadEvent.EventModel;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("upload-file-event")]
public record UploadFileEvent
{
    public FileStreamEvent FileStreamEvent { get; set; } = null!;
}
