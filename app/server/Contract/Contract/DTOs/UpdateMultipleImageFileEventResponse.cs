namespace Contract.DTOs;

public class UpdateMultipleImageFileEventResponseDTO
{
    public List<UploadImageFileEventResponseDTO>? Files { get; set; }
    public string? DeleteResponse { get; set; }
}