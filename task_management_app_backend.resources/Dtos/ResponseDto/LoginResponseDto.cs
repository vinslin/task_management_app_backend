
using System.Security.Principal;

namespace task_management_app_backend.resources.Dtos.ResponseDto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public string userName { get; set; }

        public string email { get; set; }

        public string role { get; set; }

    }
}
