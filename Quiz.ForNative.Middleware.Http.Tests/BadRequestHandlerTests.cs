using FluentAssertions;
using Newtonsoft.Json;
using Quiz.Dtos;
using Quiz.ForNative.Repository.Exceptions;
using Quiz.ForNative.Tests.Utils.Http;
using System.Net;

namespace Quiz.ForNative.Middleware.Http.Tests
{
    [TestClass]
    public class BadRequestHandlerTests
    {
        [TestMethod]
        public async Task WhenHttpClientGetResponseWithConflictStatusThenThrowError()
        {
            // Arrange
            ApiErrorDto apiError = new ApiErrorDto(400, "una error", "une error", []);
            var handlerMock = HttpMessageHandlerMock.CreateHttpMessageHandler(HttpStatusCode.BadRequest, new StringContent(JsonConvert.SerializeObject(apiError)));

            var badRequestHandler = new BadRequestHandler
            {

                InnerHandler = handlerMock.Object
            };

            var httpClient = new HttpClient(badRequestHandler);

            // Act
            Func<Task> action = async () => await httpClient.GetAsync("https://www.google.com");

            // Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        [TestMethod]
        public async Task WhenHttpClientGetResponseWithOkStatusThenDoesNotThrowError()
        {
            // Arrange
            var handlerMock = HttpMessageHandlerMock.CreateHttpMessageHandler(HttpStatusCode.OK, new StringContent("An ok status"));

            var badRequestHandler = new BadRequestHandler
            {

                InnerHandler = handlerMock.Object
            };

            var httpClient = new HttpClient(badRequestHandler);

            // Act
            Func<Task> action = async () => await httpClient.GetAsync("https://www.google.com");

            // Assert
            await action.Should().NotThrowAsync<ValidationException>();
        }
    }
}
