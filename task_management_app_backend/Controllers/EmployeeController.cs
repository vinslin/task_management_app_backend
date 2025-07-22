using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.services.IServices;

namespace task_management_app_backend.api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;


        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpGet]

        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _employeeService.GetAllEmployeeAsync();
            return Ok(result);
        }


        [Authorize(Roles = "Manager,Director")]
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto dto)
        {
            var result = await  _employeeService.AddEmployeeAsync(dto);
            return Ok(result);
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpPut("UpdateEmployee/{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] CreateEmployeeDto dto)
        {
            var result = await _employeeService.UpdateEmployeeAsync(id, dto);
            return Ok(result);

        }

        [Authorize(Roles = "Manager,Director")]
        [HttpGet("GetEmployeeTasks/{id:guid}")]
        public async Task<IActionResult> GetEmployeeTasks(Guid id)
        {
            var result =await _employeeService.GetEmployeeTasksAsync(id);
            if (result == null)
            {
                return NotFound($"No tasks found for employee with ID {id}.");
            }
            return Ok(result);

        }

        [Authorize(Roles = "Manager,Director")]
        [HttpDelete("deleteemployee/{id:guid}")]
        public async Task<IActionResult> deleteEmployee(Guid id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (result == false)
            {
                return NotFound($"No employee with ID {id}.");
            }
            return Ok(result);

        }

        [Authorize(Roles = "Manager,Director")]
        [HttpGet("Getemployeeforscroller")]
        public async Task<IActionResult> GetEmployeeForScrollBar()
        {
             
     
            return Ok(await _employeeService.GetEmployeeScrollAsync());

        }


        [Authorize(Roles = "Director")]
        [HttpGet("getsingleemployee/{id:guid}")]
        public async Task<IActionResult> getSingleEmployee(Guid id)
        {
            var result = await _employeeService.GetEmployeeAsync(id);
            if (result == null)
            {
                return NotFound($"No employee with ID {id}.");
            }
            return Ok(result);

        }

    }
}
