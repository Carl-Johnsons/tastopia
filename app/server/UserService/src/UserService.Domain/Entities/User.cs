using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[Table("User")]
public class User : BaseEntity
{
    [Required]
    public Guid AccountId { get; set; }

    public string? AvatarUrl { get; set; } = null!;

    public string? BackgroundUrl { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string? Gender  { get; set; } = null!;
}
