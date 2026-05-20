using EmployeesManager.Application.UseCases.EmployeeUseCases.CreateEmployee;
using EmployeesManager.Application.UseCases.EmployeeUseCases.SendWelcomeEmail;

namespace EmployeesManager.ProducerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(
    CreateEmployeeHandle createEmployeeHandle,
    IMessagePublisher messagePublisher): ControllerBase
{
    private readonly string queueName = "employee_created_queue";
    
    [HttpPost("Create")]
    public async Task<IActionResult> Create(
        CreateEmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        var createResponse = await createEmployeeHandle
            .HandleAsync(employeeDto, cancellationToken);

        if (!createResponse.Success) return Ok(createResponse);
        
        var employeeEvent = new EmployeeCreatedEvent(
            createResponse.Data,
            employeeDto.Name,
            employeeDto.Email,
            DateTime.Now);
            
        await messagePublisher.PublishAsync(employeeEvent, queueName, cancellationToken);
        return Ok(createResponse);
    }
}