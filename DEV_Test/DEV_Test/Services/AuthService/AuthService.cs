using DEV_Test.Controllers.DTO;
using DEV_Test.Models;
using DEV_Test.Services.AuthService.Models;
using Microsoft.Extensions.Options;

namespace DEV_Test.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<ConnectionApi> _connectionApi;
        private readonly HttpClient _httpClient;

        public AuthService(IOptions<ConnectionApi> connectionApi, HttpClient httpClient)
        {
            _connectionApi = connectionApi;
            _httpClient = httpClient;
        }
        public async Task<LoginResponse> LoginAsync(LoginModelDTO loginModel)
        {
            string url = _connectionApi.Value.ConnectionString;
            if (!string.IsNullOrEmpty(url))
            {
                url += "/auth/login";
            }

            var loginRequest = new { loginModel.Username, loginModel.Password };
            var response = await _httpClient.PostAsJsonAsync(url, loginRequest);
            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return loginResponse;
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }
        }

    }
}