

using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.RecipeEvent;

[EntityName("UpdateRecipeTagsEvent")]
public class UpdateRecipeTagsEvent
{
    [Required]
    public Guid RecipeId;

    [Required]
    public List<string> TagCodes = null!; 
}
