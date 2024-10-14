namespace Contract.Event.UploadEvent.EventModel;

public class FileStreamEvent
{
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public byte[] Stream { get; set; } = null!;
}
