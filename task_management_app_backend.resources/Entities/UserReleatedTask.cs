using System.ComponentModel.DataAnnotations.Schema;
using System;
using System;

namespace task_management_app_backend.data.Entities
{
    public class UserReleatedTask
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid TaskId { get; set; }
        public Task Task { get; set; }
    }
}
