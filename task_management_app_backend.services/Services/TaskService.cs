using AutoMapper;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;
using task_management_app_backend.services.IServices;

namespace task_management_app_backend.services.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskRelatedProjectRepository _taskRelatedProjectRepository;
        private readonly IUserRelatedTaskRepository _userRelatedTaskRepository;
        private readonly IMapper _mapper;

        public TaskService(
            ITaskRepository taskRepository,
            ITaskRelatedProjectRepository taskRelatedProjectRepository,
            IUserRelatedTaskRepository userRelatedTaskRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _taskRelatedProjectRepository = taskRelatedProjectRepository;
            _userRelatedTaskRepository = userRelatedTaskRepository;
            _mapper = mapper;
        }

        public ResponseCreateTaskDto AddTask(CreateTaskDto dto)
        {
            var task = _mapper.Map<data.Entities.Task>(dto);
            task.DueDate = DateTime.UtcNow.AddDays(dto.DaysForCompletion);
            task.CreatedAt = DateTime.UtcNow;

            var newTask = _taskRepository.Add(task);

            _userRelatedTaskRepository.Add(new UserReleatedTask
            {
                EmployeeId = dto.EmployeeId,
                TaskId = newTask.ID
            });

            _taskRelatedProjectRepository.Add(new TaskRelatedProject
            {
                TaskId = newTask.ID,
                ProjectId = dto.ProjectId
            });

            var result = _mapper.Map<ResponseCreateTaskDto>(newTask);
            result.EmployeeId = dto.EmployeeId;
            result.ProjectId = dto.ProjectId;
            result.DaysForCompletion = dto.DaysForCompletion;

            return result;
        }

        public List<ResponseCreateTaskDto> GetAllTasks()
        {
            var tasks = _taskRepository.GetAll();
            return _mapper.Map<List<ResponseCreateTaskDto>>(tasks);
        }

        public data.Entities.Task CompleteTask(Guid id)
        {
            var task = _taskRepository.GetElementById(id);
            if (task == null)
                throw new Exception("Task not found");

            task.IsCompleted = 1;
            return _taskRepository.Update(task);
        }

        public List<ResponseCreateTaskDto> GetCompletedTasks(int n)
        {
            var tasks = _taskRepository.GetAll().Where(t => t.IsCompleted == n).ToList();
            return _mapper.Map<List<ResponseCreateTaskDto>>(tasks);
        }

        public List<ResponseCreateTaskDto> GetTasksDueThisWeek()
        {
            var today = DateTime.UtcNow.Date;
            var endOfWeek = today.AddDays(7 - (int)today.DayOfWeek);

            var tasks = _taskRepository.GetAll()
                .Where(t => t.DueDate.Date >= today && t.DueDate.Date <= endOfWeek && t.IsCompleted != 1)
                .ToList();

            return _mapper.Map<List<ResponseCreateTaskDto>>(tasks);
        }
    }
}
