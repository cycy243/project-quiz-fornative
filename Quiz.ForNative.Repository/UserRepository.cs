using Newtonsoft.Json;
using Quiz.Dtos;
using Quiz.ForNative.Args;
using Quiz.ForNative.Repository.Interfaces;
using System.Net.Http.Json;

namespace Quiz.ForNative.Repository
{
    public class UserRepository : IRepository<UserDto, UserSearchArgs>
    {
        private HttpClient _httpClient { get; init; }

        public UserRepository(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Add(UserDto entity)
        {
            return (await _httpClient.PostAsJsonAsync(new Uri("/user"), entity)).IsSuccessStatusCode;
        }

        public async Task<bool> Delete(UserDto entity)
        {
            return (await _httpClient.DeleteAsync(new Uri($"/user/{entity.Id}"))).IsSuccessStatusCode;
        }

        public async Task<UserDto> Update(UserDto entity)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(new Uri($"/user/{entity.Id}"), entity);
            return JsonConvert.DeserializeObject<UserDto>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(new Uri($"/user"));
            return JsonConvert.DeserializeObject<IEnumerable<UserDto>>(await response.Content.ReadAsStringAsync())!;
        }

        public Task<IEnumerable<UserDto>> Search(UserSearchArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
