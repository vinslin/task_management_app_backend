using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.services.IServices;
using task_management_app_backend.services.Services;

namespace task_management_app_backend.api.Controllers
{

    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly IProjectService _projectService;


        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpGet]

        public async Task<IActionResult> GetAllProjects()
        {
            var result = _projectService.GetAllProjects();
            return Ok(result);
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpPost]
        public async Task<IActionResult> AddProjects(CreateProjectDto dto)
        {
            var result = _projectService.AddProject(dto);
            return Ok(result);
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpPut("UpdateProject/{id:guid}")]
        public async Task<IActionResult> UpdateProject(Guid id, CreateProjectDto dto)
        {
            var result = _projectService.UpdateProject(id, dto);
            return Ok(result);

        }

        [Authorize(Roles = "Manager,Director")]
        [HttpDelete("deleteProject/{id:guid}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var result = _projectService.deleteProject(id);
            return Ok(result);

        }


        [Authorize(Roles = "Manager,Director")]
        [HttpGet("Getprojectforscroller")]
        public async Task<IActionResult> GetProjectForScrollBar()
        {


            return Ok(_projectService.getProjectScroll());

        }


        [Authorize(Roles = "Director")]
        [HttpGet("getoneproject/{id:guid}")]
        public async Task<IActionResult> getOneProject(Guid id)
        {
            var result = _projectService.getOneProject(id);
            return Ok(result);

        }
    }
}
