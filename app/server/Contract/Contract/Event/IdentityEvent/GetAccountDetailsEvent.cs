using MassTransit;

namespace Contract.Event.IdentityEvent;

[EntityName("GetAccountDetailsEvent")]
public record GetAccountDetailsEvent
{
    public Guid AccountId { get; set; }
}
