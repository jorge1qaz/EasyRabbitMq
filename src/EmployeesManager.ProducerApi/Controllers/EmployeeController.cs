using EmployeesManager.Infrastructure.UseCases.Employee.CreateEmployeeUseCase;

namespace EmployeesManager.ProducerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(
    IMessagePublisher messagePublisher): ControllerBase
{
    private readonly string queueName = "employee_create_queue";
    
    [HttpPost("Create")]
    public async Task<IActionResult> Create(
        CreateEmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        var employeeId = Guid.NewGuid();
        var employeeEvent = new EmployeeCreatedEvent(
            employeeId,
            employeeDto.Name,
            employeeDto.Email,
            DateTime.Now);
        
        await messagePublisher.PublishAsync(employeeEvent, queueName, cancellationToken);
        return Ok(new { Message = $"Empleado registrado y enviado a la cola. Empleado = {employeeEvent.Name}" });
    }
}