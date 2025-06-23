using System;

namespace task_management_app_backend.data.Entities
{
    public class Employee
    {
        public Guid ID { get; set; }

        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }

        // Many-to-many: one employee assigned to many tasks
        public ICollection<UserReleatedTask> UserTasks { get; set; } = new List<UserReleatedTask>();

        public void SetUpdated() => UpdatedAt = DateTime.UtcNow;
        public void SetCreated() => CreatedAt = DateTime.UtcNow;
    }
}
