using task_management_app_backend.resources.Dtos.MiddleDto;


namespace task_management_app_backend.resources.Dtos.ResponseDto
{
    public class EmployeeTasks
    {
        public List<CompletedTasks> completedTasks { get; set; }

        public List<CompletedTasks> timeHavingTasks { get; set; }

        public List<CompletedTasks> dueTasks  { get; set; }
    }
}
