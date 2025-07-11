using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_management_app_backend.resources.Dtos.MiddleDto
{
    public class TaskWithDetailsDTO
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

        public List<string> ProjectNames { get; set; }
        public List<string> EmployeeNames { get; set; }
    }

}
