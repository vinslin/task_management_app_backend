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
            var result = await _taskService.AddTaskAsync(dto);
            return Ok(result);
        }




        [Authorize(Roles = "Manager,Director")]
        [HttpGet("GetAllTasks")]

        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _taskService.GetAllTasksAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Director")]
        [HttpGet("getcompletedtasks")]
        public async Task<IActionResult> GetCompletedTasks()
        {
            var result = await _taskService.GetCompletedTasksAsync(1);
            return Ok(result);
        }

        [Authorize(Roles = "Director")]
        [HttpGet("getincompletedtasks")]
        public async Task<IActionResult> GetInCompletedTasks()
        {
            var result =await  _taskService.GetCompletedTasksAsync(0);
            return Ok(result);
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpPut("Complete_Tasks/{id:Guid}")]

        public async Task<IActionResult> CompleteTask(Guid id)
        {
            var result = await _taskService.CompleteTaskAsync(id);
            if (result == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            return Ok(result);
        }


        [Authorize(Roles = "Manager,Director")]
        [HttpPut("UnComplete_Tasks/{id:Guid}")]

        public async Task<IActionResult> UnCompleteTask(Guid id)
        {
            var result = await _taskService.UnCompleteTaskAsync(id);
            if (result == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            return Ok(result);
        }


        [Authorize(Roles = "Manager,Director")]
        [HttpGet("TaskDueThisWeek")]

        public async Task<IActionResult> DueThisWeek()
        {
                var result =await  _taskService.GetTasksDueThisWeekAsync();
                if (result == null || !result.Any())
                {
                    return NotFound("No tasks due this week.");
                }
                return Ok(result);
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto dto)
        {
            try
            {
                var result = await _taskService.UpdateTaskAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Director,Manager")]
        [HttpDelete("deleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                var result = await _taskService.DeleteTaskAsync(id);
                return Ok(new { message = "Task deleted successfully", taskId = result.ID });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Director")]
        [HttpGet("getduetasks")]
        public async Task<IActionResult> dueTask()
        {
            try
            {
                var result = await _taskService.GetDueTasksAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Director")]
        [Authorize(Roles = "Director")]
        [HttpGet("gettimehavingtasks")]
        public async Task<IActionResult> getTimeHavingTask()
        {
            try
            {
                var result = await _taskService.GetTimeOneAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Director")]
        [HttpGet("employeetasks/{id}")]
        public async Task<IActionResult> EmployeeTasks(Guid id)
        {
            try
            {
                var result = await _taskService.EmployeeTasksServiceAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Director")]
        [HttpGet("gettaskbyid/{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            try
            {
                var result = await _taskService.GetTaskByIdServiceAsync(id);
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
                var result = await _taskService.ProjectTaskServiceAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }





    }
}
