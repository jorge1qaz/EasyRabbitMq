namespace EmployeesManager.Application.UseCases.EmployeeUseCases.CreateEmployee;

public class CreateEmployeeValidator: AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Name is required");
        
        RuleFor(dto => dto.Email)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Email is required");
    }
}