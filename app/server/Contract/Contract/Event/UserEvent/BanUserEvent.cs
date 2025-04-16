using MassTransit;
namespace Contract.Event.UserEvent;
[EntityName("BanUserEvent")]
public class BanUserEvent
{
    public Guid AccountId { get; set; }
}
