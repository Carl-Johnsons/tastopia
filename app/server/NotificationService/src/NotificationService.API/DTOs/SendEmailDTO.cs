namespace NotificationService.API.DTOs;

public record SendEmailDTO
{
    public string EmailTo { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
}
