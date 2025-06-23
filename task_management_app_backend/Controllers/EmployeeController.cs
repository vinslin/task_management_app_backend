using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.services.IServices;

namespace task_management_app_backend.api.Controllers
{

    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> AddEmployee(CreateEmployeeDto dto)
        {
            var result = _employeeService.AddEmployeeAsync(dto);
            return Ok(result);
        }

        [HttpPut("UpdateEmployee/{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, CreateEmployeeDto dto)
        {
            var result =  _employeeService.UpdateEmployee(id, dto);
            return Ok(result);

        }
    }
}
