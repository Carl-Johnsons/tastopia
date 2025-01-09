using Contract.DTOs.UploadFileDTO;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("UploadMultipleImageFileEvent")]
public record UploadMultipleImageFileEvent
{
    public List<FileStreamDTO> FileStreams { get; set; } = null!;
}
