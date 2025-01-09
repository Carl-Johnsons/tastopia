using Contract.DTOs.UploadFileDTO;
using MassTransit;

namespace Contract.Event.UploadEvent;

[EntityName("UpdateMultipleImageFileEvent")]
public record UpdateMultipleImageFileEvent
{
    public List<FileStreamDTO>? FileStreams { get; set; } 
    public List<string>? DeleteUrls { get; set; }
}
