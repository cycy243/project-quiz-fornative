using FluentAssertions;
using Moq;
using Quiz.Dtos;
using Quiz.ForNative.Repository.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace Quiz.ForNative.Repository.Tests
{
    [TestClass]
    public class AuthApiRepositoryTests
    {
        public required Mock<IQuizHttpClient> _mockedHttpClient;
        public required IAuthRepository<UserDto> _authRepository;

        [TestInitialize]
        public void SetUp()
        {
            _mockedHttpClient = new Mock<IQuizHttpClient>();
            _mockedHttpClient
                .Setup(mhc => mhc.PostAsync<UserDto>(It.IsAny<string>(), It.IsAny<HttpContent>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    Content = JsonContent.Create(new UserDto("", "", "", "", "", "")),
                    StatusCode = HttpStatusCode.OK
                });
            _authRepository = new AuthApiRepository(_mockedHttpClient.Object);
        }

        [TestMethod]
        public async Task WhenRegisterUserAndRegisterIsSuccessFullThenReturnUser()
        {
            // Create a temporary file
            string tempFilePath = Path.GetTempFileName();

            try
            {
                // Open a FileStream for the temporary file
                using (var fileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    // Arrange
                    UserDto? dto = null;

                    // Act
                    Func<Task> action = async () => dto = await _authRepository.RegisterUser(new UserDto("", "", "", "", "", "", "", "", ""), fileStream);

                    // Assert
                    await action.Should().NotThrowAsync();
                    dto.Should().NotBeNull();
                }
            }
            finally
            {
                // Clean up: Delete the temporary file
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }

        [TestMethod]
        public async Task WhenResponseStatusIsOKThenReturnDto()
        {
            // Arrange
            UserDto? dto = null;
            _mockedHttpClient
                .Setup(mhc => mhc.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    Content = JsonContent.Create(new UserDto("", "", "", "", "", "")),
                    StatusCode = HttpStatusCode.OK
                });

            // Act
            Func<Task> action = async () => dto = await _authRepository.GetUserByCredentialsAsync(new CredentialsArgs("login", "password"));

            // Assert
            await action.Should().NotThrowAsync();
            dto.Should().NotBeNull();
        }
    }
}
