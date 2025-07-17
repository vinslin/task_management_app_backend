using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.services.IServices;
using Microsoft.AspNetCore.Authorization;

namespace task_management_app_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;


        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpPost]
        public async Task<IActionResult> AddTask(CreateTaskDto dto)
        {
            var result = _taskService.AddTask(dto);
            return Ok(result);
        }




        [Authorize(Roles = "Manager,Director")]
        [HttpGet("GetAllTasks")]

        public async Task<IActionResult> GetAllTasks()
        {
            var result = _taskService.GetAllTasks();
            return Ok(result);
        }

        [HttpGet("getcompletedtasks")]
        public async Task<IActionResult> GetCompletedTasks()
        {
            var result = _taskService.GetCompletedTasks(1);
            return Ok(result);
        }

        [HttpGet("getincompletedtasks")]
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
        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto dto)
        {
            try
            {
                var result = _taskService.UpdateTask(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("deleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                var result = _taskService.DeleteTask(id);
                return Ok(new { message = "Task deleted successfully", taskId = result.ID });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getduetasks")]
        public async Task<IActionResult> dueTask()
        {
            try
            {
                var result = _taskService.GetDueTasks();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("gettimehavingtasks")]
        public async Task<IActionResult> getTimeHavingTask()
        {
            try
            {
                var result = _taskService.getTimeOne();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("employeetasks/{id}")]
        public async Task<IActionResult> EmployeeTasks(Guid id)
        {
            try
            {
                var result = _taskService.employeeTasksService(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("gettaskbyid/{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            try
            {
                var result = _taskService.getTaskByIdService(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Director")]
        [HttpGet("projecttasks/{id}")]
        public async Task<IActionResult> ProjectTasks(Guid id)
        {
            try
            {
                var result = _taskService.projectTaskService(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }





    }
}
