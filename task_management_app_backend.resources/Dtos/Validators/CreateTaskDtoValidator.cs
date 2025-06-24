using FluentValidation;
using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.resources.Dtos.Validators
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto> 
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(x => x.Priority)
                .IsInEnum()
                .Must(p => ((int)p) >= 1 && ((int)p) <= 3)
                .WithMessage("Priority must be a valid value between 1 and 3.");

            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .Must(id => id != Guid.Empty)
                .WithMessage("Project ID is required and must be a valid GUID.");

            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .Must(id => id != Guid.Empty)
                .WithMessage("Employee ID is required and must be a valid GUID.");

        }

    }
}
