using task_management_app_backend.resources.Dtos.MiddleDto;


namespace task_management_app_backend.resources.Dtos.ResponseDto
{
    public class EmployeeTasks
    {
        public List<CompletedTasks> completedTasks { get; set; }

        public List<TimeHavingTasks> timeHavingTasks { get; set; }

        public List<DueTasks> dueTasks  { get; set; }
    }
}
