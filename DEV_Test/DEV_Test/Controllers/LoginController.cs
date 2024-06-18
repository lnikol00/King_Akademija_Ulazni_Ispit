using DEV_Test.Controllers.DTO;
using DEV_Test.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace DEV_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModelDTO loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Invalid login request");
            }

            try
            {
                var token = await _authService.LoginAsync(loginModel);
                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password");
            }
        }
    }
}
