using global::task_management_app_backend.resources.Dtos.RequestDto;
using global::task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.services.IServices
{


  
        public interface ITaskService
        {
            ResponseCreateTaskDto AddTask(CreateTaskDto dto);
        }
    

}
