using task_management_app_backend.data.Entities;
using task_management_app_backend.data.Enums;
namespace task_management_app_backend.data.Entities
{
    public class Task
    {
        public Guid ID { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public PriorityLevel Priority { get; set; } = PriorityLevel.Low;
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }
        public int IsCompleted { get; set; } = 0;

        public ICollection<TaskRelatedProject> TaskProjects { get; set; } = new List<TaskRelatedProject>();
        public ICollection<UserReleatedTask> 
            UserTasks { get; set; } = new List<UserReleatedTask>();

        public void SetUpdated() => UpdatedAt = DateTime.UtcNow;
        public void SetCreated() => CreatedAt = DateTime.UtcNow;
    }

}