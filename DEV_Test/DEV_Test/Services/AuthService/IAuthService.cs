using DEV_Test.Controllers.DTO;
using DEV_Test.Services.AuthService.Models;

namespace DEV_Test.Services.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginModelDTO loginModel);
    }
}