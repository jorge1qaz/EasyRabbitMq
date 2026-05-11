namespace EmployeesManager.Infrastructure.Messaging;

public class RabbitMqPublisher(
    IRabbitMqConnection persistentConnection): IMessagePublisher
{
    public async Task PublishAsync<T>(T message, string queueName, CancellationToken ct = default) 
        where T : class
    {
        await using var connection = await persistentConnection.GetConnectionAsync(ct);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: ct);

        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: ct);
        
        var json = JsonSerializer.Serialize(message);
        var body = System.Text.Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: queueName,
            body: body,
            cancellationToken: ct);
    }
}