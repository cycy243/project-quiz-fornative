using FluentAssertions;
using Moq;
using Moq.Protected;
using Quiz.Dtos.Api;
using Quiz.ForNative.Repository.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace Quiz.ForNative.Repository.Tests
{
    [TestClass]
    public class AuthApiRepositoryTests
    {
        public Mock<IQuizHttpClient> _mockedHttpClient;

        [TestInitialize]
        public void SetUp()
        {
            _mockedHttpClient = new Mock<IQuizHttpClient>();
            _mockedHttpClient
                .Setup(mhc => mhc.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<UserDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage()
                {
                    Content = JsonContent.Create(new UserDto("", "", "", "", "", "")),
                    StatusCode = HttpStatusCode.OK
                });
        }

        [TestMethod]
        public async Task WhenRegisterUserAndRegisterIsSuccessFullThenReturnUser()
        {
            // Arrange
            UserDto? dto = null;
            IAuthRepository<UserDto> authRepository = new AuthApiRepository(_mockedHttpClient.Object);

            // Act
            Func<Task> action = async () => dto = await authRepository.RegisterUser(new UserDto("", "", "", "", "", ""));

            // Assert
            await action.Should().NotThrowAsync();
            dto.Should().NotBeNull();
        }
    }
}
