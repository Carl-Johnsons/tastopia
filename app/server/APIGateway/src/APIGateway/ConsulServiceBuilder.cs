using Consul;
using Ocelot.Discovery.Consul;
using Ocelot.Logging;

namespace APIGateway;

public class ConsulServiceBuilder : DefaultConsulServiceBuilder
{
    public ConsulServiceBuilder(
        IHttpContextAccessor contextAccessor,
        IConsulClientFactory clientFactory,
        IOcelotLoggerFactory loggerFactory)
        : base(contextAccessor, clientFactory, loggerFactory)
    {
    }

    protected override string GetDownstreamHost(
        ServiceEntry entry,
        Node node)
        => entry.Service.Address;
}
