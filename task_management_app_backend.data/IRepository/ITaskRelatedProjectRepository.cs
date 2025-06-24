using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.Data;

namespace task_management_app_backend.data.IRepository
{
    public interface ITaskRelatedProjectRepository
    {
        public bool Add(TaskRelatedProject task);
        public TaskRelatedProject Update(TaskRelatedProject task);
        // Additional methods can be added as needed, such as GetById, GetAll, etc.
    }
}
