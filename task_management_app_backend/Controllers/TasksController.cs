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


    }
}
