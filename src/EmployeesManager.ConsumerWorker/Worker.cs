using EmployeesManager.Infrastructure.Messaging;

namespace EmployeesManager.ConsumerWorker;

public class Worker(
    ILogger<Worker> logger
    ) : BackgroundService
{
    private readonly string queueName = "employee_create_queue";
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
    }
}