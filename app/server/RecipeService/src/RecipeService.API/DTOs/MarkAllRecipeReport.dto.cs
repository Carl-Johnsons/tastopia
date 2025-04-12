using System.ComponentModel.DataAnnotations;

namespace RecipeService.API.DTOs;

public class MarkAllRecipeReportDTO
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public bool IsReopened { get; set; }
}
