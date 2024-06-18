using DEV_Test.Controllers.DTO;

namespace DEV_Test.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginModelDTO loginModel);
    }
}