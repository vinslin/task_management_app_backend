using AutoMapper;
using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.Enums;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.resources.Dtos.MiddleDto;
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
            var result = _mapper.Map<List<ResponseCreateTaskDto>>(tasks);
            return (result);
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

        public List<ResponseCreateTaskDto> GetDueTasks()
        {
            var today = DateTime.UtcNow.Date;
           // var endOfWeek = today.AddDays(7 - (int)today.DayOfWeek);

            var tasks = _taskRepository.GetAll()
                .Where(t => t.DueDate.Date < today && t.IsCompleted != 1)
                .ToList();

            return _mapper.Map<List<ResponseCreateTaskDto>>(tasks);
        }

        public async Task<ResponseCreateTaskDto> UpdateTask(UpdateTaskDto dto)
        {
            var task = _taskRepository.GetElementById(dto.ID);
            if (task == null)
                throw new Exception("Task not found");

            // Update fields
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Priority = (PriorityLevel)dto.Priority;
            task.DueDate = dto.DueDate;
            task.IsCompleted = dto.IsCompleted;
            task.SetUpdated(); // update UpdatedAt

            var updatedTask = _taskRepository.Update(task);

            // Update Employee
            var userRelation = _userRelatedTaskRepository
                .GetAll()
                .FirstOrDefault(r => r.TaskId == task.ID);

            if (userRelation != null)
            {
                // Delete old FK relation
                _userRelatedTaskRepository.Delete(userRelation);
            }

            // Always add the new one
            _userRelatedTaskRepository.Add(new UserReleatedTask
            {
                TaskId = dto.ID,
                EmployeeId = dto.EmployeeId
            });

            // Update Project
            var projectRelation = _taskRelatedProjectRepository
              .GetAll()
              .FirstOrDefault(r => r.TaskId == task.ID);

            if (projectRelation != null)
            {
                // Remove old FK relation
                _taskRelatedProjectRepository.Delete(projectRelation);
            }

            // Always add the new one
            _taskRelatedProjectRepository.Add(new TaskRelatedProject
            {
                TaskId = dto.ID,
                ProjectId = dto.ProjectId
            });


            var result = _mapper.Map<ResponseCreateTaskDto>(updatedTask);
            result.EmployeeId = dto.EmployeeId;
            result.ProjectId = dto.ProjectId;

            return result;
        }
        public data.Entities.Task DeleteTask(Guid id)
        {
            var task = _taskRepository.GetElementById(id);
            if (task == null)
                throw new Exception("Task not found");

            // Delete Task ↔ Employee relation
            var userRelation = _userRelatedTaskRepository
                .GetAll()
                .FirstOrDefault(r => r.TaskId == id);
            if (userRelation != null)
            {
                _userRelatedTaskRepository.Delete(userRelation);
            }

            // Delete Task ↔ Project relation
            var projectRelation = _taskRelatedProjectRepository
                .GetAll()
                .FirstOrDefault(r => r.TaskId == id);
            if (projectRelation != null)
            {
                _taskRelatedProjectRepository.Delete(projectRelation);
            }

            // Delete Task
            return _taskRepository.Delete(task);
        }

        public List<ResponseCreateTaskDto> getTimeOne() {
            var today = DateTime.UtcNow.Date;
            // var endOfWeek = today.AddDays(7 - (int)today.DayOfWeek);

            var tasks = _taskRepository.GetAll()
                .Where(t => t.DueDate.Date >= today && t.IsCompleted != 1)
                .ToList();

            return _mapper.Map<List<ResponseCreateTaskDto>>(tasks);

        }

        public EmployeeTasks employeeTasksService(Guid id)
        {
            var userTasks = _userRelatedTaskRepository.employeeReleatedTasks(id);

            var completedTasks = userTasks
                .Where(ut => ut.Task != null && ut.Task.IsCompleted == 1)
                .Select(ut => new CompletedTasks
                {
                    taskId = ut.Task.ID,
                    taskName = ut.Task.Title
                })
                .ToList();

            var timeHavingTasks = userTasks
                .Where(ut => ut.Task != null && ut.Task.DueDate > DateTime.Now)
                .Select(ut => new CompletedTasks
                {
                    taskId = ut.Task.ID,
                    taskName = ut.Task.Title
                   
                })
                .ToList();

            var dueTasks = userTasks
                .Where(ut => ut.Task != null && ut.Task.DueDate <= DateTime.Now && ut.Task.IsCompleted == 0)
                .Select(ut => new CompletedTasks
                {
                    taskId = ut.Task.ID,
                    taskName = ut.Task.Title
                })
                .ToList();

            return new EmployeeTasks
            {
                completedTasks = completedTasks,
                timeHavingTasks = timeHavingTasks,
                dueTasks = dueTasks
            };
        }

        public ResponseCreateTaskDto getTaskByIdService(Guid id) {

            var tasks = _taskRepository.GetOne(id);
            var result = _mapper.Map<ResponseCreateTaskDto>(tasks);
            return (result);


        }

        public ProjectTasks projectTaskService(Guid id) {
            var projectTasks = _taskRelatedProjectRepository.projectReleatedTasks(id);

            var completedTasks = projectTasks
                .Where(ut => ut.Task != null && ut.Task.IsCompleted == 1)
                .Select(ut => new CompletedTasks
                {
                    taskId = ut.Task.ID,
                    taskName = ut.Task.Title
                })
                .ToList();

            var timeHavingTasks = projectTasks
                .Where(ut => ut.Task != null && ut.Task.DueDate > DateTime.Now)
                .Select(ut => new CompletedTasks
                {
                    taskId = ut.Task.ID,
                    taskName = ut.Task.Title

                })
                .ToList();

            var dueTasks = projectTasks
                .Where(ut => ut.Task != null && ut.Task.DueDate <= DateTime.Now && ut.Task.IsCompleted == 0)
                .Select(ut => new CompletedTasks
                {
                    taskId = ut.Task.ID,
                    taskName = ut.Task.Title
                })
                .ToList();

            return new ProjectTasks
            {
                completedTasks = completedTasks,
                timeHavingTasks = timeHavingTasks,
                dueTasks = dueTasks
            };
        }
    }
}
