
using Contract.Event.NotificationEvent;
using EmailWorker.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EmailWorker;

public class Worker : IHostedLifecycleService
{
    private readonly ILogger<Worker> _logger;
    private readonly RabbitMQProvider _rabbitmqProvider;
    private readonly IEmailService _emailService;
    private IConnection _connection;
    private IChannel _channel;
    private readonly string EXCHANGE_NAME = "email-worker-exchange";
    private readonly string QUEUE_NAME = "email-worker-queue";

    public Worker(ILogger<Worker> logger, RabbitMQProvider rabbitmqProvider, IEmailService emailService)
    {
        _logger = logger;
        _rabbitmqProvider = rabbitmqProvider;
        _emailService = emailService;
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

            await _channel.ExchangeDeclareAsync(
                exchange: EXCHANGE_NAME,
                type: ExchangeType.Fanout,
                durable: true
            );
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
            var rawMessage = Encoding.UTF8.GetString(body);
            try
            {
                var outerJson = JsonConvert.DeserializeObject<JObject>(rawMessage);
                var innerMessageJson = outerJson?["message"]?.ToString();
                if (string.IsNullOrWhiteSpace(innerMessageJson))
                {
                    throw new Exception("Message property is missing or empty in the JSON payload");
                }

                var emailObj = JsonConvert.DeserializeObject<SendEmailEvent>(innerMessageJson);

                if (emailObj == null)
                {
                    throw new Exception("Email Object is null");
                }

                await _emailService.SendEmail(
                            emailTo: emailObj.EmailTo,
                            subject: emailObj.Subject,
                            body: emailObj.Body,
                            isHtml: true);

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
