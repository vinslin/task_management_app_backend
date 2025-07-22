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
            var result = await _projectService.GetAllProjectsAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpPost]
        public async Task<IActionResult> AddProjects(CreateProjectDto dto)
        {
            var result = await _projectService.AddProjectAsync(dto);
            return Ok(result);
        }

        [Authorize(Roles = "Manager,Director")]
        [HttpPut("UpdateProject/{id:guid}")]
        public async Task<IActionResult> UpdateProject(Guid id, CreateProjectDto dto)
        {
            var result = await _projectService.UpdateProjectAsync(id, dto);
            return Ok(result);

        }

        [Authorize(Roles = "Manager,Director")]
        [HttpDelete("deleteProject/{id:guid}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            return Ok(result);

        }


        [Authorize(Roles = "Manager,Director")]
        [HttpGet("Getprojectforscroller")]
        public async Task<IActionResult> GetProjectForScrollBar()
        {


            return Ok(await _projectService.GetProjectScrollAsync());

        }


        [Authorize(Roles = "Director")]
        [HttpGet("getoneproject/{id:guid}")]
        public async Task<IActionResult> getOneProject(Guid id)
        {
            var result = await _projectService.GetOneProjectAsync(id);
            return Ok(result);

        }
    }
}
