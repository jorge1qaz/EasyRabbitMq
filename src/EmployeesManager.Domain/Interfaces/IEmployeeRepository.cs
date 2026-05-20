namespace EmployeesManager.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task CreateAsync(Employee employee, CancellationToken ct);
}