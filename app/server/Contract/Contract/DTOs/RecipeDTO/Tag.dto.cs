using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.RecipeDTO;

public class TagListDTO
{
    [Required]
    public List<TagDTO> Tags { get; set; } = null!;
}

public class TagDTO
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Value { get; set; } = null!;

    [Required]
    public string Code { get; set; } = null!;

    [Required]
    public string Category { get; set; } = null!;

    [Required]
    public string Status { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

}
