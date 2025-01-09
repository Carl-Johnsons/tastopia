using Contract.DTOs.UploadFileDTO;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("UpdateFileEvent")]
public record UpdateFileEvent
{
    public string Url { get; set; } = null!;
    public FileStreamDTO FileStream { get; set; } = null!;
}
