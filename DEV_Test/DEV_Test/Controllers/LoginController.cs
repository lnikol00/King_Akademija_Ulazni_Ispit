using DEV_Test.Controllers.DTO;
using DEV_Test.Services.AuthService;
using DEV_Test.Services.AuthService.Models;
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

        // Endpoint radi login za korisnika, te ako je login uspješaj kreira AccessToken za njega i vraća osnovne podatke o korisniku
        // Primjer zahtjeva:
        // POST https://localhost:7197/api/Login
        // Primjer korisnika : Username: "emilys", Password: "emilyspass"
        // Ukoliko korisnik ne postoji, endpoint vraća Unauthorized.
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModelDTO loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Invalid login request");
            }

            try
            {
                var user = await _authService.LoginAsync(loginModel);
                return Ok(user);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password");
            }
        }
    }
}
