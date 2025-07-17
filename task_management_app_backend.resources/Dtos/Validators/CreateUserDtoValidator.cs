using FluentValidation;

using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.resources.Dtos.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        { 
            RuleFor(x => x.userName)
                .NotEmpty().WithMessage("User name is required.")
                .MinimumLength(3).WithMessage("User name must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("User name must not exceed 50 characters.");
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(role => role == "Director" || role == "Manager")
                .WithMessage("Role must be either 'Director' or 'Manager'.");

            RuleFor(x => x.passWord)
                    .NotEmpty().WithMessage("Password is required.")
                    .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                    .MaximumLength(100).WithMessage("Password must not exceed 100 characters.");

        }
    
    }
}
