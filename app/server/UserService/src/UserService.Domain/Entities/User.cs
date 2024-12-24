using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[Table("User")]
public class User : BaseEntity
{
    [Required]
    public string DisplayName { get; set; } = null!;

    [Required]
    public string AvatarUrl { get; set; } = null!;

    [Required]
    public string BackgroundUrl { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string? Gender  { get; set; }

    public string? Bio { get; set; }

    public string? Address { get; set; }

    //Viture

    public virtual int? TotalFollwer {  get; set; }

    public virtual int? TotalFollowing { get; set; }

    public virtual int? TotalRecipe { get; set; }



}
