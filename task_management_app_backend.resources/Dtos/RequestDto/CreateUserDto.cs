
namespace task_management_app_backend.resources.Dtos.RequestDto
{
    public class CreateUserDto
    {
        public string userName { get; set; }

        public string email { get; set; }

        public string passWord { get; set; }

        public string role { get; set; }
    }
}
