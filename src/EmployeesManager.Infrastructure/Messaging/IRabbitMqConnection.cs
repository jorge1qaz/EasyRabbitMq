namespace EmployeesManager.Infrastructure.Messaging;

public interface IRabbitMqConnection
{
    Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default);
}