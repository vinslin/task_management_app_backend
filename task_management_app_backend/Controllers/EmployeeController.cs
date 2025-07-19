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
            var result = _employeeService.GetAllEmployee();
            return Ok(result);
        }


        [Authorize(Roles = "Manager,Director")]
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto dto)
        {
            var result =  _employeeService.AddEmployeeAsync(dto);
            return Ok(result);
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpPut("UpdateEmployee/{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] CreateEmployeeDto dto)
        {
            var result = _employeeService.UpdateEmployee(id, dto);
            return Ok(result);

        }

        [Authorize(Roles = "Manager,Director")]
        [HttpGet("GetEmployeeTasks/{id:guid}")]
        public async Task<IActionResult> GetEmployeeTasks(Guid id)
        {
            var result = _employeeService.GetEmployeeTasks(id);
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
            var result = _employeeService.deleteEmployee(id);
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
             
     
            return Ok(_employeeService.getEmployeeScroll());

        }


        [Authorize(Roles = "Director")]
        [HttpGet("getsingleemployee/{id:guid}")]
        public async Task<IActionResult> getSingleEmployee(Guid id)
        {
            var result = _employeeService.getEmployee(id);
            if (result == null)
            {
                return NotFound($"No employee with ID {id}.");
            }
            return Ok(result);

        }

    }
}
