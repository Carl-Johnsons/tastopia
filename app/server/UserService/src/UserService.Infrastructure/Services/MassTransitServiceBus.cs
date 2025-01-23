using MassTransit;
using Microsoft.Extensions.Logging;

namespace UserService.Infrastructure.Services;

public class MassTransitServiceBus : IServiceBus
{
    private readonly IBus _bus;
    private readonly ILogger<MassTransitServiceBus> _logger;

    public MassTransitServiceBus(IBus bus, ILogger<MassTransitServiceBus> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task Publish<T>(T eventMessage) where T : class
    {
        await _bus.Publish(eventMessage);
        _logger.LogInformation($"Publish event {typeof(T).Name} successfully");
    }
}
