using FluentValidation;
using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.resources.Dtos.Validators
{
    public class CreateEmployeeDtoValidator  : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .Length(3, 50).WithMessage("UserName must be between 3 and 50 characters.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Length(3, 20).WithMessage("Role must be between 3 and 20 characters.");
        }
    }
}
