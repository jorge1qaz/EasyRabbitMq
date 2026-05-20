namespace EmployeesManager.Application.Common;

public interface IHandler<in TData, TResponse>
{
    Task<Result<TResponse>> HandleAsync(TData data, CancellationToken ct);
}