using Newtonsoft.Json;
using Quiz.Dtos.Api;
using Quiz.ForNative.Repository.Interfaces;

namespace Quiz.ForNative.Repository
{
    public class AuthApiRepository : IAuthRepository<UserDto>
    {
        private IQuizHttpClient _httpClient { get; init; }

        public AuthApiRepository(IQuizHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto> RegisterUser(UserDto user)
        {
            HttpResponseMessage response = (await _httpClient.PostAsJsonAsync("/auth/register", user));
            return JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync())!;
        }
    }
}
