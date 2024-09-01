using Newtonsoft.Json;
using Quiz.Dtos;
using Quiz.ForNative.Repository.Http;
using Quiz.ForNative.Repository.Interfaces;
using System.IO;
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
            MultipartFormDataContent multipartFormContent = new MultipartFormDataContent
            {
                { new StringContent(user.Bio), nameof(UserDto.Bio).ToLower() },
                { new StringContent(user.Birthday!), "birthDate" },
                { new StringContent(user.Firstname), nameof(UserDto.Firstname).ToLower() },
                { new StringContent(user.Name), nameof(UserDto.Name).ToLower() },
                { new StringContent(user.Email), nameof(UserDto.Email).ToLower() },
                { new StringContent(user.Pseudo), nameof(UserDto.Pseudo).ToLower() },
                { new StringContent(user.Password!), nameof(UserDto.Password).ToLower() }
            };
            StreamContent fileStreamContent = new StreamContent(fileStream);
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            multipartFormContent.Add(fileStreamContent, name: "file", fileName: $"{user.Pseudo}.png");
            HttpResponseMessage response = (await _httpClient.PostAsync<UserDto>("/auth", multipartFormContent));
            return await response.GetContentAsync<UserDto>()!;
        }

        public async Task<UserDto?> GetUserByCredentialsAsync(CredentialsArgs args)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/auth/connect", new { login = args.Login, password = args.Password });
            return await response.GetContentAsync<UserDto>()!;
        }
    }
}
