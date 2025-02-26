using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Serilog;

namespace SignalRHub.Filters;

public class GlobalLoggingFilter : IHubFilter
{
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async ValueTask<object> InvokeMethodAsync(
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
    {
        Log.Information($"Calling hub method '{invocationContext.HubMethodName}'");
        // Log the parameters
        if (invocationContext.HubMethodArguments != null && invocationContext.HubMethodArguments.Count > 0)
        {
            Log.Information($"Parameters: {JsonConvert.SerializeObject(invocationContext.HubMethodArguments, Formatting.Indented)}");
        }
        try
        {
            return await next(invocationContext);
        }
        catch (Exception ex)
        {
            Log.Information($"Exception calling '{invocationContext.HubMethodName}': {ex}");
            throw;
        }
    }
    // Optional method
    public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
    {
        return next(context);
    }

    // Optional method
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    public Task OnDisconnectedAsync(
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        HubLifetimeContext context, Exception exception, Func<HubLifetimeContext, Exception, Task> next)
    {
        return next(context, exception);
    }
}
