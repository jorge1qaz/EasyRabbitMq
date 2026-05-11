using EmployeesManager.Application.Extensions;
using EmployeesManager.ConsumerWorker;
using EmployeesManager.Infrastructure.Extension;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = builder.Configuration;
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddApplicationServices();

var host = builder.Build();
host.Run();