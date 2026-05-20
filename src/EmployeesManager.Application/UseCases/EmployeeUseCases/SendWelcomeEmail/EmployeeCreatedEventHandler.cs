namespace EmployeesManager.Application.UseCases.EmployeeUseCases.SendWelcomeEmail;

public class EmployeeCreatedEventHandler(
    ILogger<EmployeeCreatedEventHandler> logger): IIntegrationEventHandler<EmployeeCreatedEvent>
{
    public async Task HandleAsync(
        EmployeeCreatedEvent messageStructure, CancellationToken token = default)
    {
        logger.LogInformation("Evento recibido: EmployeeCreatedEvent");
        logger.LogInformation("Empleado: {MessageStructureName}", messageStructure.Name);
        logger.LogInformation("Enviando correo a: {MessageStructureEmail}", messageStructure.Email);
        
        await Task.Delay(5000, token);
        logger.LogInformation("Correo enviado correctamente a {MessageStructureEmail}", messageStructure.Email);
    }
}