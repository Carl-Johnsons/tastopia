

using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Contract.Event.RecipeEvent;

[EntityName("RequestAddTagsEvent")]
public class RequestAddTagsEvent
{
    [Required]
    public List<string> Requests { get; set; } = null!;

    [Required]
    public Guid RecipeId { get; set; }
}
