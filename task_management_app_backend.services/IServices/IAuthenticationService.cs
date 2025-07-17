using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;
namespace task_management_app_backend.services.IServices
{
    public interface IAuthenticationService
    {


        public bool Register(CreateUserDto newUser);

        public LoginResponseDto Login(LoginRequestDto loginRequest);
    }
}
