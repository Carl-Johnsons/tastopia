namespace Contract.Common;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class QueueNameAttribute : Attribute
{
    public string QueueName { get; }
    public string ExchangeName { get; }

    public QueueNameAttribute(string queueName, string exchangeName = null)
    {
        QueueName = queueName;
        ExchangeName = exchangeName;
    }
}
