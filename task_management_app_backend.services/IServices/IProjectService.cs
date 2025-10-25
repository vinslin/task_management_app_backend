using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.services.IServices
{
    public interface IProjectService
    {

        public Project AddProject(CreateProjectDto projectDto);
        public List<Project> GetAllProjects();
        public Project UpdateProject(Guid id , CreateProjectDto project);
        public Project GetProjectById(Guid id);

        public bool deleteProject(Guid id);

        public List<getProjectsScrollbarDto> getProjectScroll();


    }
}
