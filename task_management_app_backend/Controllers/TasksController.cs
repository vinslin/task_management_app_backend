using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.services.IServices;

namespace task_management_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;


        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpPost]
        public async Task<IActionResult> AddTask(CreateTaskDto dto)
        {
            var result = _taskService.AddTask(dto);
            return Ok(result);
        }

        [HttpGet("GetAllTasks")]

        public async Task<IActionResult> GetAllTasks()
        {
            var result = _taskService.GetAllTasks();
            return Ok(result);
        }
        [HttpGet("GetCompletedTasks")]
        public async Task<IActionResult> GetCompletedTasks()
        {
            var result = _taskService.GetCompletedTasks(1);
            return Ok(result);
        }

        [HttpGet("GetInmpletedTasks")]
        public async Task<IActionResult> GetInCompletedTasks()
        {
            var result = _taskService.GetCompletedTasks(0);
            return Ok(result);
        }


        [HttpPatch("Complete_Tasks/{id:Guid}")]

        public async Task<IActionResult> CompleteTask(Guid id)
        {
            var result = _taskService.CompleteTask(id);
            if (result == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpGet("TaskDueThisWeek")]

        public async Task<IActionResult> DueThisWeek()
        {
                var result = _taskService.GetTasksDueThisWeek();
                if (result == null || !result.Any())
                {
                    return NotFound("No tasks due this week.");
                }
                return Ok(result);
        }


    }
}
