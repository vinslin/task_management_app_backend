using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_management_app_backend.resources.Dtos.RequestDto
{
    public class UpdateTaskDto
    {
        public Guid ID { get; set; } // Same as entity's Task ID
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; } // Maps to enum
        public DateTime DueDate { get; set; }
        public int IsCompleted { get; set; }

        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }
    }

}
