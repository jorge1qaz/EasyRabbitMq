namespace EmployeesManager.Application.UseCases.EmployeeUseCases.CreateEmployee;

public class CreateEmployeeHandle(
    IEmployeeRepository repository,
    //IUnitOfWork unitOfWork,
    IValidator<CreateEmployeeDto> validator): IHandler<CreateEmployeeDto, Guid>
{
    public async Task<Result<Guid>> HandleAsync(CreateEmployeeDto data, CancellationToken ct)
    {
        var validation = await validator.ValidateAsync(data, ct);

        if (!validation.IsValid)
        {
            return Result<Guid>.Fail(
                validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        var employee = new Employee()
        {
            Name = data.Name,
            Email = data.Email
        };

        try
        {
            await repository.CreateAsync(employee, ct);
            //await unitOfWork.SaveChangesAsync();
            return Result<Guid>.Ok(employee.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Fail(ex.Message);
        }
    }
}