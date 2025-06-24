
using task_management_app_backend.data.Enums;

namespace task_management_app_backend.resources.Dtos.ResponseDto
{
    public class ResponseCreateTaskDto
    {

        public required Guid TaskId { get; set; } 
        public required string Title { get; set; }
        public required string Description { get; set; }

        public int DaysForCompletion { get; set; }

        public DateTime DueDate { get; set; }

        public PriorityLevel Priority { get; set; }

        public Guid? ProjectId { get; set; }

        public Guid EmployeeId { get; set; }

        public int IsCompleted { get; set; }
    }
}
