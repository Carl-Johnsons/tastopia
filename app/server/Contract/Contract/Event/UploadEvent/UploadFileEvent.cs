using Contract.DTOs.UploadFileDTO;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("UploadFileEvent")]
public record UploadFileEvent
{
    public FileStreamDTO FileStream { get; set; } = null!;
}
