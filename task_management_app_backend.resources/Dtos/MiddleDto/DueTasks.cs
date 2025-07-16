using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_management_app_backend.resources.Dtos.MiddleDto
{
    public class DueTasks
    {
        public Guid taskId { get; set; }

        public string taskName { get; set; }

    }
}
