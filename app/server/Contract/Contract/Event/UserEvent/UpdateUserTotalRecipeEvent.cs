using MassTransit;
namespace Contract.Event.UserEvent;
[EntityName("UpdateUserTotalRecipeEvent")]
public class UpdateUserTotalRecipeEvent
{
    public Guid AccountId { get; set; }
    public int Delta { get; set; } 
}
