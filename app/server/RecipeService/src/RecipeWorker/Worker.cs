using Contract.Constants;
using Contract.Event.NotificationEvent;
using RecipeWorker.Interfaces;
namespace RecipeWorker;

public class Worker : IHostedLifecycleService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    public Task StartingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recipe worker starting");
        return Task.CompletedTask;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recipe worker start");
        try
        {
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
    public Task StartedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recipe worker started");
        return Task.CompletedTask;
    }
    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recipe worker stopping");
        return Task.CompletedTask;
    }
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recipe worker stop");
    }
    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recipe worker stopped");
        return Task.CompletedTask;
    }
}
