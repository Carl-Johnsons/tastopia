using System.ComponentModel.DataAnnotations;

namespace UserService.API.DTOs;

public class MarkAllUserReportDTO
{
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public bool IsReopened { get; set; }
}
