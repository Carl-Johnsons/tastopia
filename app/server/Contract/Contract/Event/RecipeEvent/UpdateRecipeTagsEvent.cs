

using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.RecipeEvent;

[EntityName("UpdateRecipeTagsEvent")]
public class UpdateRecipeTagsEvent
{
    [Required]
    public Guid RecipeId { get; set; }

    [Required]
    public List<string> TagCodes { get; set; } = null!; 
}
