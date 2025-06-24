
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
                TaskId = newtask.ID,
                Title = newtask.Title,
                Description = newtask.Description,
                DaysForCompletion = dto.DaysForCompletion,
                DueDate = newtask.DueDate,
                Priority = newtask.Priority,
                ProjectId = dto.ProjectId,
                EmployeeId = dto.EmployeeId
            };
           
        }

        public List<ResponseCreateTaskDto> GetAllTasks() { 
            var tasks = _taskRepository.GetAll();
            var responseTasks = new List<ResponseCreateTaskDto>();
            foreach (var task in tasks)
            {
                responseTasks.Add(new ResponseCreateTaskDto
                {
                    TaskId = task.ID,
                    Title = task.Title,
                    Description = task.Description,
                    DaysForCompletion = (int)(task.DueDate - DateTime.UtcNow).TotalDays,
                    DueDate = task.DueDate,
                    Priority = task.Priority,
                    ProjectId= task.TaskProjects.FirstOrDefault()?.ProjectId ?? Guid.Empty,
                    EmployeeId = task.UserTasks.FirstOrDefault()?.EmployeeId ?? Guid.Empty,
                    IsCompleted = task.IsCompleted 
                });
            }

            return responseTasks;

        }

        public data.Entities.Task CompleteTask(Guid id)
        {
            var task = _taskRepository.GetElementById(id);
            if (task == null)
            {
                throw new Exception("Task not found");
            }
            task.IsCompleted = 1;
            return _taskRepository.Update(task);
        }

        public List<ResponseCreateTaskDto> GetCompletedTasks(int n)
        {
            var tasks = _taskRepository.GetAll();
            var responseTasks = new List<ResponseCreateTaskDto>();
            foreach (var task in tasks)
            {
                if (task.IsCompleted == n)
                {
                    responseTasks.Add(new ResponseCreateTaskDto
                    {
                        TaskId = task.ID,
                        Title = task.Title,
                        Description = task.Description,
                        DaysForCompletion = (int)(task.DueDate - DateTime.UtcNow).TotalDays,
                        DueDate = task.DueDate,
                        Priority = task.Priority,
                        ProjectId = task.TaskProjects.FirstOrDefault()?.ProjectId ?? Guid.Empty,
                        EmployeeId = task.UserTasks.FirstOrDefault()?.EmployeeId ?? Guid.Empty,
                        IsCompleted = task.IsCompleted
                    });
                }
            }

            return responseTasks;
        }

        public List<ResponseCreateTaskDto> GetTasksDueThisWeek()
        {
            var today = DateTime.UtcNow.Date;
            var endOfWeek = today.AddDays(7 - (int)today.DayOfWeek); // Sunday is 0, Saturday is 6

            // Load all tasks with related data
            var tasks = _taskRepository.GetAll()
                .Where(t => t.DueDate.Date >= today && t.DueDate.Date <= endOfWeek 
                        && t.IsCompleted != 1)
                .ToList();

            var responseTasks = tasks.Select(task => new ResponseCreateTaskDto
            {
                TaskId = task.ID,
                Title = task.Title,
                Description = task.Description,
                DaysForCompletion = (int)(task.DueDate - DateTime.UtcNow).TotalDays,
                DueDate = task.DueDate,
                Priority = task.Priority,
                ProjectId = task.TaskProjects.FirstOrDefault()?.ProjectId ?? Guid.Empty,
                EmployeeId = task.UserTasks.FirstOrDefault()?.EmployeeId ?? Guid.Empty,
                IsCompleted = task.IsCompleted
            }).ToList();

            return responseTasks;
        }




    }
}
