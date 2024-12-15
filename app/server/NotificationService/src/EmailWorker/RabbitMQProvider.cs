using RabbitMQ.Client;

namespace EmailWorker;

public class RabbitMQProvider : IAsyncDisposable
{
    private IConnection _connection;
    private readonly ConnectionFactory _connectionFactory;
    private readonly ILogger<RabbitMQProvider> _logger;

    public RabbitMQProvider(ConnectionFactory connectionFactory, ILogger<RabbitMQProvider> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task<IConnection> GetConnectionAsync()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
            _logger.LogInformation($"RabbitMQ connected: {_connectionFactory.Endpoint}");
        }
        return _connection;
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null && _connection.IsOpen)
        {
            try
            {
                _logger.LogInformation("RabbitMQ disconnecting");
                await _connection.CloseAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while closing RabbitMQ connection");
            }
            finally
            {
                _connection.Dispose();
            }
        }
    }
}
