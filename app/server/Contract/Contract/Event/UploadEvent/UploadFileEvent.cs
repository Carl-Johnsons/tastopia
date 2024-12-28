using Contract.Event.UploadEvent.EventModel;
using MassTransit;

namespace Contract.Event.UploadEvent;

public record UploadFileEvent
{
    public FileStreamEvent FileStreamEvent { get; set; } = null!;
}
