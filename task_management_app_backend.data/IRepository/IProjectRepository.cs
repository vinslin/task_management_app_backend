using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.data.IRepository
{
    public interface IProjectRepository
    {
        public Project Add(CreateProjectDto projectDto);
        public List<Project> GetAll();
        public Project Update(Project project);
        public Project GetProjectById(Guid id);
    }
}
