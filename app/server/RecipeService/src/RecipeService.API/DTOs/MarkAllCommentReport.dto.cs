using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class MarkAllCommentReportDTO
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public Guid CommentId { get; set; }

    [Required]
    public bool IsReopened { get; set; }
}
