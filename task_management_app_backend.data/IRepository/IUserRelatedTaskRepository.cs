using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_management_app_backend.data.Entities;

namespace task_management_app_backend.data.IRepository
{
    public interface IUserRelatedTaskRepository
    {

        public bool Add(UserReleatedTask task);
        public UserReleatedTask Update(UserReleatedTask task);

    }
}
