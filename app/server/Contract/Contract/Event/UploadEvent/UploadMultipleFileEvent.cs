using Contract.DTOs.UploadFileDTO;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("UploadMultipleFileEvent")]
public record UploadMultipleFileEvent
{
    public List<FileStreamDTO> FileStreams { get; set; } = null!;
}
