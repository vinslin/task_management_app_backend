using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace task_management_app_backend.data.Entities
{
    public class TaskRelatedProject
    {
        public Guid TaskId { get; set; }
        public Task Task { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }

}
