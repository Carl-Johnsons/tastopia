namespace Contract.DTOs.UploadFileDTO;

public class FileDTO
{
    public string PublicId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public long Size { get; set; }
    public string Url { get; set; } = null!;
}

public class ListFileDTO
{
    public List<FileDTO> Files { get; set; } = null!;
}
