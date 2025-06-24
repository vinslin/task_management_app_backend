
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.Enums;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.data.Repository;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;
using task_management_app_backend.services.IServices;


namespace task_management_app_backend.services.Services
{
    public class TaskService :ITaskService 
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskRelatedProjectRepository _taskRelatedProjectRepository;
        private readonly IUserRelatedTaskRepository _userRelatedTaskRepository;

        public TaskService(ITaskRepository taskRepository, ITaskRelatedProjectRepository taskRelatedProjectRepository, IUserRelatedTaskRepository userRelatedTaskRepository) {
            _taskRepository = taskRepository;
            _taskRelatedProjectRepository = taskRelatedProjectRepository;
            _userRelatedTaskRepository = userRelatedTaskRepository;

        }

        public ResponseCreateTaskDto AddTask(CreateTaskDto dto)
        {
            var task = new data.Entities.Task
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = DateTime.UtcNow.AddDays(dto.DaysForCompletion),
                Priority = dto.Priority,

                CreatedAt = DateTime.UtcNow
          
            };

            var newtask = _taskRepository.Add(task);

         
            var userTask = new UserReleatedTask {
                EmployeeId = dto.EmployeeId,
                TaskId = newtask.ID
            };

            bool db1 = _userRelatedTaskRepository.Add(userTask);

            var taskProject = new TaskRelatedProject { 
                      TaskId = newtask.ID,
                      ProjectId = dto.ProjectId
            };

            bool db2 = _taskRelatedProjectRepository.Add(taskProject);



            return new ResponseCreateTaskDto
            {
                Title = newtask.Title,
                Description = newtask.Description,
                DaysForCompletion = dto.DaysForCompletion,
                DueDate = newtask.DueDate,
                Priority = newtask.Priority,
                ProjectId = dto.ProjectId,
                EmployeeId = dto.EmployeeId
            };
           
        }



    }
}
