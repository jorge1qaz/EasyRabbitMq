namespace EmployeesManager.Application.Common.Interfaces;

public interface IMessageConsumer<T> where T: class
{
    Task StartConsumerAsync(string queueName, CancellationToken ct);
}