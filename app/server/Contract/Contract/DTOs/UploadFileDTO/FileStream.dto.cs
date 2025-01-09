namespace Contract.DTOs.UploadFileDTO;

public class ListFileStreamDTO
{
    public List<FileStreamDTO> FileStreams { get; set; } = [];
}

public class FileStreamDTO
{
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public byte[] Stream { get; set; } = null!;
}
