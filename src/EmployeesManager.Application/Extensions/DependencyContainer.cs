namespace EmployeesManager.Application.Extensions;

public static class DependencyContainer
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventHandler<EmployeeCreatedEvent>, EmployeeCreatedEventHandler>();
        
        return services;
    }
}