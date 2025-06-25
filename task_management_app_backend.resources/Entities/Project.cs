using task_management_app_backend.data.Entities;

public class Project
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public ICollection<TaskRelatedProject> ProjectTasks { get; set; } = new List<TaskRelatedProject>();

    public void SetCreated() => CreatedAt = DateTime.UtcNow;
    public void SetUpdated() => UpdatedAt = DateTime.UtcNow;
}
