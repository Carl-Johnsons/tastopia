namespace UserService.API.DTOs;
public class ErrorResponseDTO
{
    public int Status { get; set; }
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;
}
