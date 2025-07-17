using FluentValidation;
using System.Security.Cryptography.X509Certificates;
using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.resources.Dtos.Validators
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                   .NotEmpty().WithMessage("Email is required")
                   .EmailAddress().WithMessage("invalid Email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        }
    }
}
