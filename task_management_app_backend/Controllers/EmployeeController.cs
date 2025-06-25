using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.services.IServices;

namespace task_management_app_backend.api.Controllers
{

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

        [HttpGet]

        public async Task<IActionResult> GetAllEmployees()
        {
            var result = _employeeService.GetAllEmployee();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto dto)
        {
            var result = _employeeService.AddEmployeeAsync(dto);
            return Ok(result);
        }

        [HttpPut("UpdateEmployee/{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] CreateEmployeeDto dto)
        {
            var result = _employeeService.UpdateEmployee(id, dto);
            return Ok(result);

        }

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
    }
}
