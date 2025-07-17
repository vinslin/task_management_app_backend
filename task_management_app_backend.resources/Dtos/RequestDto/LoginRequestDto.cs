
namespace task_management_app_backend.resources.Dtos.RequestDto
{
    public class LoginRequestDto
    {

        public string Email { get; set; } 
        public string Password { get; set; }
        // Optional: You can add properties for RememberMe or other login-related fields if needed
        // Default to false if not provided
    }
}
