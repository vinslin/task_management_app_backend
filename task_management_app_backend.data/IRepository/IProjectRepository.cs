using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.data.IRepository
{
    public interface IProjectRepository
    {
        Project Add(Project project);
        List<Project> GetAll();
        Project Update(Project project);
        Project GetProjectById(Guid id);

        bool DeletePro(Guid id);

        List<getProjectsScrollbarDto> getProScroll();
    }
}
