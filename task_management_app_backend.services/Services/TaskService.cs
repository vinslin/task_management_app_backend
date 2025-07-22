        using AutoMapper;
        using task_management_app_backend.data.Entities;
        using task_management_app_backend.data.Enums;
        using task_management_app_backend.data.IRepository;
        using task_management_app_backend.resources.Dtos.MiddleDto;
        using task_management_app_backend.resources.Dtos.RequestDto;
        using task_management_app_backend.resources.Dtos.ResponseDto;
        using task_management_app_backend.services.IServices;
        using Microsoft.Extensions.Caching.Memory;

        namespace task_management_app_backend.services.Services
        {
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskRelatedProjectRepository _taskRelatedProjectRepository;
        private readonly IUserRelatedTaskRepository _userRelatedTaskRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public TaskService(
            ITaskRepository taskRepository,
            ITaskRelatedProjectRepository taskRelatedProjectRepository,
            IUserRelatedTaskRepository userRelatedTaskRepository,
            IMapper mapper, IMemoryCache cache)
        {
            _taskRepository = taskRepository;
            _taskRelatedProjectRepository = taskRelatedProjectRepository;
            _userRelatedTaskRepository = userRelatedTaskRepository;
            _mapper = mapper;
            _cache = cache;
        }

        private async Task<List<data.Entities.Task>> GetCachedTasksAsync()
        {
            const string cacheKey = "AllTasks";

            if (!_cache.TryGetValue(cacheKey, out List<data.Entities.Task> cachedTasks))
            {
                var tasks = await _taskRepository.GetAllAsync();
                cachedTasks = tasks;

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _cache.Set(cacheKey, cachedTasks, cacheEntryOptions);
            }

            return cachedTasks;
        }

        private void InvalidateTaskCache()
        {
            _cache.Remove("AllTasks");
        }

        public async Task<ResponseCreateTaskDto> AddTaskAsync(CreateTaskDto dto)
        {
            var task = _mapper.Map<data.Entities.Task>(dto);
            task.DueDate = DateTime.UtcNow.AddDays(dto.DaysForCompletion);
            task.CreatedAt = DateTime.UtcNow;

            var newTask = await _taskRepository.AddAsync(task);

            await _userRelatedTaskRepository.AddAsync(new UserReleatedTask
            {
                EmployeeId = dto.EmployeeId,
                TaskId = newTask.ID
            });

            await  _taskRelatedProjectRepository.AddAsync(new TaskRelatedProject
            {
                TaskId = newTask.ID,
                ProjectId = dto.ProjectId
            });

            InvalidateTaskCache();

            var result = _mapper.Map<ResponseCreateTaskDto>(newTask);
            result.EmployeeId = dto.EmployeeId;
            result.ProjectId = dto.ProjectId;
            result.DaysForCompletion = dto.DaysForCompletion;

            return result;
        }

        public async Task<List<ResponseCreateTaskDto>> GetAllTasksAsync()
        {
            var tasks = await GetCachedTasksAsync();
            var result = _mapper.Map<List<ResponseCreateTaskDto>>(tasks);
            return result;
        }

        public async Task<data.Entities.Task> CompleteTaskAsync(Guid id)
        {
            var task = await _taskRepository.GetElementByIdAsync(id);
            if (task == null)
                throw new Exception("Task not found");

            task.IsCompleted = 1;
            InvalidateTaskCache();
            return await _taskRepository.UpdateAsync(task);
        }

        public async Task<List<ResponseCreateTaskDto>> GetCompletedTasksAsync(int n)
        {
            var tasks = (await GetCachedTasksAsync()).Where(t => t.IsCompleted == n).ToList();
            return _mapper.Map<List<ResponseCreateTaskDto>>(tasks);
        }

        public async Task<List<ResponseCreateTaskDto>> GetTasksDueThisWeekAsync()
        {
            var today = DateTime.UtcNow.Date;
            var endOfWeek = today.AddDays(7 - (int)today.DayOfWeek);

            var tasks = (await GetCachedTasksAsync())
                .Where(t => t.DueDate.Date >= today && t.DueDate.Date <= endOfWeek && t.IsCompleted != 1)
                .ToList();

            return _mapper.Map<List<ResponseCreateTaskDto>>(tasks);
        }

        public async Task<List<ResponseCreateTaskDto>> GetDueTasksAsync()
        {
            var today = DateTime.UtcNow.Date;

            var tasks = (await GetCachedTasksAsync())
                .Where(t => t.DueDate.Date < today && t.IsCompleted != 1)
                .ToList();

            return _mapper.Map<List<ResponseCreateTaskDto>>(tasks);
        }

        public async Task<ResponseCreateTaskDto> UpdateTaskAsync(UpdateTaskDto dto)
        {
            var task = await _taskRepository.GetElementByIdAsync(dto.ID);
            if (task == null)
                throw new Exception("Task not found");

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Priority = (PriorityLevel)dto.Priority;
            task.DueDate = dto.DueDate;
            task.IsCompleted = dto.IsCompleted;
            task.SetUpdated();

            var updatedTask = await _taskRepository.UpdateAsync(task);

            var userRelations = await _userRelatedTaskRepository.GetAllAsync();
            var userRelation = userRelations.FirstOrDefault(r => r.TaskId == task.ID);
            if (userRelation != null)
            {
                await _userRelatedTaskRepository.DeleteAsync(userRelation);
            }

            await _userRelatedTaskRepository.AddAsync(new UserReleatedTask
            {
                TaskId = dto.ID,
                EmployeeId = dto.EmployeeId
            });

            var projectRelations = await _taskRelatedProjectRepository.ProjectReleatedTasksAsync(task.ID);
            var projectRelation = projectRelations.FirstOrDefault(r => r.TaskId == task.ID);
            if (projectRelation != null)
            {
                await  _taskRelatedProjectRepository.DeleteAsync(projectRelation);
            }

            await  _taskRelatedProjectRepository.AddAsync(new TaskRelatedProject
            {
                TaskId = dto.ID,
                ProjectId = dto.ProjectId
            });

            InvalidateTaskCache();

            var result = _mapper.Map<ResponseCreateTaskDto>(updatedTask);
            result.EmployeeId = dto.EmployeeId;
            result.ProjectId = dto.ProjectId;

            return result;
        }

        public async Task<data.Entities.Task> DeleteTaskAsync(Guid id)
        {
            var task = await _taskRepository.GetElementByIdAsync(id);
            if (task == null)
                throw new Exception("Task not found");

            var userRelations = await _userRelatedTaskRepository.GetAllAsync();
            var userRelation = userRelations.FirstOrDefault(r => r.TaskId == id);
            if (userRelation != null)
            {
                await _userRelatedTaskRepository.DeleteAsync(userRelation);
            }

            var projectRelations = await _taskRelatedProjectRepository.ProjectReleatedTasksAsync(id);
            var projectRelation = projectRelations.FirstOrDefault(r => r.TaskId == id);
            if (projectRelation != null)
            {
                await  _taskRelatedProjectRepository.DeleteAsync(projectRelation);
            }

            InvalidateTaskCache();

            return await _taskRepository.DeleteAsync(task);
        }

        public async Task<List<ResponseCreateTaskDto>> GetTimeOneAsync()
        {
            var today = DateTime.UtcNow.Date;

            var tasks = (await GetCachedTasksAsync())
                .Where(t => t.DueDate.Date >= today && t.IsCompleted != 1)
                .ToList();

            return _mapper.Map<List<ResponseCreateTaskDto>>(tasks);
        }

        public async Task<EmployeeTasks> EmployeeTasksServiceAsync(Guid id)
        {
            var userTasks = await _userRelatedTaskRepository.EmployeeReleatedTasksAsync(id);

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

        public async Task<ResponseCreateTaskDto> GetTaskByIdServiceAsync(Guid id)
        {
            var task = await _taskRepository.GetOneAsync(id);
            var result = _mapper.Map<ResponseCreateTaskDto>(task);
            return result;
        }

        public async Task<ProjectTasks> ProjectTaskServiceAsync(Guid id)
        {
            var projectTasks = await  _taskRelatedProjectRepository.ProjectReleatedTasksAsync(id);

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
