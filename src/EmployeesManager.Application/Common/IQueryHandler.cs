namespace EmployeesManager.Application.Common;

public interface IQueryHandler<in TData, TResponse>
{
    Task<IEnumerable<TResponse>> HandleAsync(TData data, CancellationToken ct);
}