using global::task_management_app_backend.resources.Dtos.RequestDto;
using global::task_management_app_backend.resources.Dtos.ResponseDto;
using global::task_management_app_backend.data.Entities;

namespace task_management_app_backend.services.IServices
{


  
public interface ITaskService
    {
        Task<ResponseCreateTaskDto> AddTaskAsync(CreateTaskDto dto);
        Task<List<ResponseCreateTaskDto>> GetAllTasksAsync();

        Task<GetAllTaskPaginationDto> GetAllTaskWithPaginationAsync(int pageNumber,int pageSize,string ? serchString = null, string? sortBy = null, bool isAscending = true);

        Task<data.Entities.Task> CompleteTaskAsync(Guid id);

        Task<data.Entities.Task> UnCompleteTaskAsync(Guid id);
        Task<List<ResponseCreateTaskDto>> GetCompletedTasksAsync(int n);
        Task<List<ResponseCreateTaskDto>> GetTasksDueThisWeekAsync();
        Task<ResponseCreateTaskDto> UpdateTaskAsync(UpdateTaskDto dto);
        Task<data.Entities.Task> DeleteTaskAsync(Guid id);
        Task<List<ResponseCreateTaskDto>> GetDueTasksAsync();
        Task<List<ResponseCreateTaskDto>> GetTimeOneAsync();
        Task<EmployeeTasks> EmployeeTasksServiceAsync(Guid id);
        Task<ResponseCreateTaskDto> GetTaskByIdServiceAsync(Guid id);
        Task<ProjectTasks> ProjectTaskServiceAsync(Guid id);


    }


}
