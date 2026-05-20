namespace EmployeesManager.Application.UseCases.EmployeeUseCases.SendWelcomeEmail;

public record EmployeeCreatedEvent(
    Guid Id,
    string Name,
    string Email,
    DateTime CreatedAt);