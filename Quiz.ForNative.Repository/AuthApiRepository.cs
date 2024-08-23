using Newtonsoft.Json;
using Quiz.Dtos.App;
using Quiz.ForNative.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository
{
    public class AuthApiRepository : IAuthRepository<UserDto>
    {
        private HttpClient _httpClient { get; init; }

        public AuthApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto> RegisterUser(UserDto user)
        {
            HttpResponseMessage response = (await _httpClient.PostAsJsonAsync(new Uri("/auth/register"), user));
            return JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync())!;
        }
    }
}
