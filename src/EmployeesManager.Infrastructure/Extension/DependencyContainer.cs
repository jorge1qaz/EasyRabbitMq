using EmployeesManager.Infrastructure.Messaging;

namespace EmployeesManager.Infrastructure.Extension;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(
            configuration.GetSection(RabbitMqOptions.SectionName));
        services.AddSingleton<IRabbitMqConnection, PersistentConnection>();

        services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
        
        return services;
    }
}