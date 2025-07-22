using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;
namespace task_management_app_backend.services.IServices
{
    public interface IAuthenticationService
    {


        public Task<bool> RegisterAsync(CreateUserDto newUser);

        public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
    }
}
