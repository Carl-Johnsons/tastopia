namespace EmailWorker;

public class Worker : IHostedLifecycleService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    public Task StartingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email worker starting");
        return Task.CompletedTask;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email worker start");
        try
        {
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        return Task.CompletedTask;
    }
    public Task StartedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email worker started");
        return Task.CompletedTask;
    }
    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email worker stopping");
        return Task.CompletedTask;
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email worker stop");
        return Task.CompletedTask;
    }
    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email worker stopped");
        return Task.CompletedTask;
    }
}
