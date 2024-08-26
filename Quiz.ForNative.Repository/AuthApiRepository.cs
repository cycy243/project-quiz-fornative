using Newtonsoft.Json;
using Quiz.Dtos;
using Quiz.ForNative.Repository.Interfaces;
using System.Net.Http.Headers;

namespace Quiz.ForNative.Repository
{
    public class AuthApiRepository : IAuthRepository<UserDto>
    {
        private IQuizHttpClient _httpClient { get; init; }

        public AuthApiRepository(IQuizHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto> RegisterUser(UserDto user, FileStream fileStream)
        {
            // TODO : Handle error 400 (maybe with a middleware)
            // TODO : Create DTO to get api errors
            MultipartFormDataContent multipartFormContent = new MultipartFormDataContent
            {
                { new StringContent(user.Bio), nameof(UserDto.Bio).ToLower() },
                { new StringContent(user.Birthday), "birthDate" },
                { new StringContent(user.Firstname), nameof(UserDto.Firstname).ToLower() },
                { new StringContent(user.Name), nameof(UserDto.Name).ToLower() },
                { new StringContent(user.Email), nameof(UserDto.Email).ToLower() },
                { new StringContent(user.Pseudo), nameof(UserDto.Pseudo).ToLower() },
                { new StringContent(user.Password), nameof(UserDto.Password).ToLower() }
            };
            StreamContent fileStreamContent = new StreamContent(fileStream);
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            multipartFormContent.Add(fileStreamContent, name: "file", fileName: $"{user.Pseudo}.png");
            HttpResponseMessage response = (await _httpClient.PostAsync<UserDto>("/auth", multipartFormContent));
            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDto>(responseContent)!;
        }
    }
}
