using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.services.IServices;

namespace task_management_app_backend.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly IProjectService _projectService;


        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllProjects()
        {
            var result = _projectService.GetAllProjects();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProjects(CreateProjectDto dto)
        {
            var result = _projectService.AddProject(dto);
            return Ok(result);
        }

        [HttpPut("UpdateProject/{id:guid}")]
        public async Task<IActionResult> UpdateProject(Guid id, CreateProjectDto dto)
        {
            var result = _projectService.UpdateProject(id, dto);
            return Ok(result);

        }
    }
}
