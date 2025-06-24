using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.data.IRepository
{
    public interface ITaskRepository
    {
        public Entities.Task Add(Entities.Task task);

        public List<Entities.Task> GetAll();

        public Entities.Task Update(Entities.Task task);

        public Entities.Task GetElementById(Guid id);
    }
}
