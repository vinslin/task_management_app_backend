
namespace task_management_app_backend.resources.Dtos.ResponseDto
{
    public class GetTaskByEmployeeIdDto
    {
        public required Guid TaskId { get; set; } 
        public required string Title { get; set; }
        public required string Description { get; set; }

        public int IsCompleted { get; set; }
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        // Assuming Priority is an integer value
        public Guid? ProjectId { get; set; }
       

       // Assuming IsCompleted is an integer value (0 or 1)
    }
}
