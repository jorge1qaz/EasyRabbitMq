using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;

namespace EmployeesManager.Infrastructure.Messaging;

public class RabbitMqConsumer<T>(
    ILogger<RabbitMqConsumer<T>> logger,
    IRabbitMqConnection persistantConnection,
    IServiceScopeFactory scopeFactory): IMessageConsumer<T> where T: class
{
    public async Task StartConsumerAsync(string queueName, CancellationToken ct)
    {
        var connection = await persistantConnection.GetConnectionAsync(ct);
        var channel = await connection.CreateChannelAsync(cancellationToken: ct);

        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: ct);
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                var eventStructure = JsonSerializer.Deserialize<T>(message);

                if (eventStructure != null)
                {
                    using var scope = scopeFactory.CreateScope();
                    var handler = scope.ServiceProvider
                        .GetRequiredService<IIntegrationEventHandler<T>>();
                    await handler.HandleAsync(eventStructure, ct);
                }
                
                await channel.BasicAckAsync(ea.DeliveryTag, false, ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                
                // En caso de que haya fallado lo devolvemos a la cola
                await channel.BasicNackAsync(ea.DeliveryTag, false, requeue:true, ct);
            }
        };
        
        await channel.BasicConsumeAsync(queueName, false, consumer, cancellationToken: ct);
    }
}