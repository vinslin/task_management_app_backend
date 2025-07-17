using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.services.IServices;

namespace task_management_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public IActionResult Register(CreateUserDto dto)
        {
            var result = _authService.Register(dto);
            return Ok(result);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequestDto dto)
        {
            var result = _authService.Login(dto);
            return Ok(result);
        }
    }
}
