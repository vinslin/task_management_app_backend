using global::task_management_app_backend.resources.Dtos.RequestDto;
using global::task_management_app_backend.resources.Dtos.ResponseDto;
using global::task_management_app_backend.data.Entities;

namespace task_management_app_backend.services.IServices
{


  
        public interface ITaskService
        {
            ResponseCreateTaskDto AddTask(CreateTaskDto dto);
           // List<ResponseCreateTaskDto> UpdateTask();

        List<ResponseCreateTaskDto> GetAllTasks();

        data.Entities.Task CompleteTask(Guid id);
        List<ResponseCreateTaskDto> GetCompletedTasks(int n);

        List<ResponseCreateTaskDto> GetTasksDueThisWeek();
    }
    

}
