namespace EmployeesManager.ConsumerWorker;

public class Worker(
    ILogger<Worker> logger,
    IMessageConsumer<EmployeeCreatedEvent> consumer) : BackgroundService
{
    private readonly string queueName = "employee_created_queue";
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Iniciando cola: {QueueName}", queueName);
        
        await consumer.StartConsumerAsync(queueName, stoppingToken);
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}