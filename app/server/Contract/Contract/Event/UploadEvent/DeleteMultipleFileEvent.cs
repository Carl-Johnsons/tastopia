using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("DeleteMultipleFileEvent")]
public record DeleteMultipleFileEvent
{
    public List<string> DeleteUrl { get; set; } = null!;
}
