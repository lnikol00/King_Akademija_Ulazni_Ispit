using DEV_Test.Controllers.DTO;
using DEV_Test.Models;
using DEV_Test.Services.AuthService.Models;
using Microsoft.Extensions.Options;

namespace DEV_Test.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<ConnectionApi> _connectionApi;

        public AuthService(IOptions<ConnectionApi> connectionApi)
        {
            _connectionApi = connectionApi;
        }
        public async Task<string> LoginAsync(LoginModelDTO loginModel)
        {
            string url = _connectionApi.Value.ConnectionString;
            if (!string.IsNullOrEmpty(url))
            {
                url += "/auth/login";
            }

            HttpClient client = new HttpClient();
            var loginRequest = new { loginModel.Username, loginModel.Password };

            var response = await client.PostAsJsonAsync(url, loginRequest);
            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return loginResponse.Token;
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }
        }
    }
}