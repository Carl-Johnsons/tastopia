using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Serilog;

namespace SignalRHub.Filters;

public class GlobalLoggingFilter : IHubFilter
{
    public async ValueTask<object> InvokeMethodAsync(
    HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
    {
        Log.Information($"Calling hub method '{invocationContext.HubMethodName}'");
        // Log the parameters
        if (invocationContext.HubMethodArguments != null && invocationContext.HubMethodArguments.Count > 0)
        {
            Log.Information("Parameters:");
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(invocationContext.HubMethodArguments));
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
    public Task OnDisconnectedAsync(
        HubLifetimeContext context, Exception exception, Func<HubLifetimeContext, Exception, Task> next)
    {
        return next(context, exception);
    }
}
