namespace EmployeesManager.Application.Common.Events;

public record EmployeeCreatedEvent(
    Guid Id,
    string Name,
    string Email,
    DateTime CreatedAt);