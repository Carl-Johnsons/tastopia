namespace IdentityService.Domain.Interfaces;
public interface IServiceBus
{
    Task Publish<T>(T eventMessage) where T : class;
}
