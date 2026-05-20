namespace EmployeesManager.Application.Common;

public class Result<T>
{
    public bool Success { get; private init; } = false;
    public T? Data { get; private init; }
    public IEnumerable<string> ErrorMessages { get; private init; } = new List<string>();
    
    private Result() {}

    public static Result<T> Ok(T data) =>
        new Result<T>
        { 
            Success = true,
            Data = data
        };

    public static Result<T> Fail(IEnumerable<string> errorMessages) =>
        new Result<T> { ErrorMessages = errorMessages.ToList() };
    
    public static Result<T> Fail(string errorMessage) =>
        new Result<T> { ErrorMessages = new List<string> { errorMessage } };
}