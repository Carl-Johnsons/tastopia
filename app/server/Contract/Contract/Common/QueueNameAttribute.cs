using Contract.Constants;

namespace Contract.Common;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class QueueNameAttribute : Attribute
{
    public string QueueName { get; init; }
    public string ExchangeName { get; init; }
    public bool AutoDelete { get; init; }
    public bool Durable { get; init; }
    public string Type { get; init; }
    public QueueNameAttribute(
        string queueName,
        string exchangeName = "",
        bool durable = false,
        string type = RabbitMQConstant.EXCHANGE.TYPE.Direct,
        bool autoDelete = true)
    {
        QueueName = queueName;
        Durable = durable;
        Type = type;
        AutoDelete = autoDelete;
        if (string.IsNullOrEmpty(exchangeName))
        {
            ExchangeName = queueName;
        }
        else
        {
            ExchangeName = exchangeName;
        }
    }
}
