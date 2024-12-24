using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("delete-multiple-file-event")]
public record DeleteMultipleFileEvent
{
    public List<string> DeleteUrl { get; set; } = null!;
}
