using FluentAssertions;
using Quiz.ForNative.Tests.Utils.Http;
using System.Net;
using Exceptions = Quiz.ForNative.Repository.Exceptions;

namespace Quiz.ForNative.Middleware.Http.Tests
{
    [TestClass]
    public class UnAuthorizedAccessHandlerTests
    {
        [TestMethod]
        public async Task WhenHttpClientGetResponseWithUnAuthorizedStatusThenThrowError()
        {
            // Arrange
            var handlerMock = HttpMessageHandlerMock.CreateHttpMessageHandler(HttpStatusCode.Unauthorized, new StringContent("Unauthorized response content"));

            var unAuthorizedAccessHandler = new UnAuthorizedAccessHandler
            {

                InnerHandler = handlerMock.Object
            };

            var httpClient = new HttpClient(unAuthorizedAccessHandler);

            // Act
            Func<Task> action = async () => await httpClient.GetAsync("https://www.google.com");

            // Assert
            await action.Should().ThrowAsync<Exceptions.UnAuthorizedAccessException>();
        }

        [TestMethod]
        public async Task WhenHttpClientGetResponseWithForbiddenStatusThenThrowError()
        {
            // Arrange
            var handlerMock = HttpMessageHandlerMock.CreateHttpMessageHandler(HttpStatusCode.Forbidden, new StringContent("Forbidden response content"));

            var unAuthorizedAccessHandler = new UnAuthorizedAccessHandler
            {

                InnerHandler = handlerMock.Object
            };

            var httpClient = new HttpClient(unAuthorizedAccessHandler);

            // Act
            Func<Task> action = async () => await httpClient.GetAsync("https://www.google.com");

            // Assert
            await action.Should().ThrowAsync<Exceptions.UnAuthorizedAccessException>();
        }

        [TestMethod]
        public async Task WhenHttpClientGetResponseWithOkStatusThenDoesNotThrowError()
        {
            // Arrange
            var handlerMock = HttpMessageHandlerMock.CreateHttpMessageHandler(HttpStatusCode.OK, new StringContent("ok response content"));

            var unAuthorizedAccessHandler = new UnAuthorizedAccessHandler
            {

                InnerHandler = handlerMock.Object
            };

            var httpClient = new HttpClient(unAuthorizedAccessHandler);

            // Act
            Func<Task> action = async () => await httpClient.GetAsync("https://www.google.com");

            // Assert
            await action.Should().NotThrowAsync<UnauthorizedAccessException>();
        }
    }
}
