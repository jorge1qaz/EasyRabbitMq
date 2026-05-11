namespace EmployeesManager.Infrastructure.Messaging;

public class PersistentConnection(IOptions<RabbitMqOptions> options) : IRabbitMqConnection
{
    private IConnection? _connection;
    private readonly SemaphoreSlim _connectionLock = new(1, 1);
    private bool _disposed;
    
    private readonly IConnectionFactory _factory = new ConnectionFactory()
    {
        HostName = options.Value.HostName,
        Port = options.Value.Port,
        UserName = options.Value.UserName,
        Password = options.Value.Password
    };

    public async Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        if (_connection is { IsOpen: true }) return _connection;
        
        await _connectionLock.WaitAsync(cancellationToken);
        try
        {
            if (_connection is { IsOpen: true }) return _connection;
            _connection = await _factory.CreateConnectionAsync(cancellationToken);
            return _connection;
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    public void Dispose()
    {
        if (!_disposed) return;
        _connection?.Dispose();
        _disposed = true;
    }
}