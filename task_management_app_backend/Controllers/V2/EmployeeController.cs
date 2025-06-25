using Microsoft.AspNetCore.Mvc;
using MediatR;
using task_management_app_backend.resources.CQRS.Commands;
using task_management_app_backend.services.CQRS.Queries;

namespace task_management_app_backend.api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> CreateEmployee([FromBody] AddEmployeeCommand command)
        {
            var employeeId = await _mediator.Send(command);
            return Ok(new { Message = "Employee created", EmployeeId = employeeId });
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(employees);
        }
    }
}
