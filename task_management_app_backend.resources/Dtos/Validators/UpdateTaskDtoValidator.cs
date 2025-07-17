using FluentValidation;
using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.resources.Dtos.Validators
{
    public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskDtoValidator()
        {
            RuleFor(x => x.ID).NotEmpty().WithMessage("task Id  required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Priority).NotEmpty().WithMessage("Priority is required.");
            RuleFor(x => x.DueDate).NotEmpty().WithMessage("DueDate is required.");
            RuleFor(x => x.IsCompleted).NotEmpty().WithMessage("IsCompleted is required.");

            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
            RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("EmployeeId is required.");
        }
    }    
    
    }

