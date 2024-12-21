using MassTransit;

namespace NotificationService.Infrastructure.EventPublishing;

public class MassTransitServiceBus : IServiceBus
{
    private readonly IBus _bus;

    public MassTransitServiceBus(IBus bus)
    {
        _bus = bus;
    }

    public IRequestClient<TRequest> CreateRequestClient<TRequest>() where TRequest : class
    {
        return _bus.CreateRequestClient<TRequest>();
    }

    public async Task Publish<T>(T eventMessage) where T : class
    {
        await _bus.Publish(eventMessage);
    }
}
