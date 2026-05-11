namespace EmployeesManager.Application.Common.Interfaces;

public interface IIntegrationEventHandler<in T> where T: class
{
    Task HandleAsync(T messageStructure, CancellationToken token = default);
}