using MassTransit;

namespace Contract.Event.RecipeEvent;

[EntityName("get-recipes-event")]
public record GetRecipesEvent
{
    public HashSet<Guid> UserIds { get; set; } = null!;
}
