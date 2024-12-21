
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EmailWorker;

public class Worker : IHostedLifecycleService
{
    private readonly ILogger<Worker> _logger;
    private readonly RabbitMQProvider _rabbitmqProvider;
    private IConnection _connection;
    private IChannel _channel;
    private readonly string EXCHANGE_NAME = "email-worker-exchange";
    private readonly string QUEUE_NAME = "email-worker-queue";

    public Worker(ILogger<Worker> logger, RabbitMQProvider rabbitmqProvider)
    {
        _logger = logger;
        _rabbitmqProvider = rabbitmqProvider;
    }
    public Task StartingAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email worker starting");
        return Task.CompletedTask;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email worker start");
        try
        {
            _connection = await _rabbitmqProvider.GetConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(EXCHANGE_NAME, ExchangeType.Direct);
            await _channel.QueueDeclareAsync(QUEUE_NAME, durable: true, exclusive: false, autoDelete: false, arguments: null);
            await _channel.QueueBindAsync(QUEUE_NAME, EXCHANGE_NAME, "email-exchange-key");
            StartListening();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
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
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email worker stop");
        await _rabbitmqProvider.DisposeAsync();
    }
    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email worker stopped");
        return Task.CompletedTask;
    }

    private async void StartListening()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            try
            {
                _logger.LogInformation($"Processed message {message}");

                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                _logger.LogInformation("Message acknowledged");
            }
            catch (Exception ex)
            {
                _logger.LogError("Message rejected: " + ex.Message);
                await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
            }
        };
        await _channel.BasicConsumeAsync(
            queue: QUEUE_NAME,
            autoAck: false,
            consumer: consumer);
        _logger.LogInformation($"Listening on queue: {QUEUE_NAME}");
    }
}
