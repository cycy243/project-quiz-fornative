using FluentAssertions;
using Moq;
using Moq.Protected;
using Quiz.ForNative.Repository.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository.Tests.Http
{
    [TestClass]
    public class QuizHttpClientTests
    {
        [TestMethod]
        public async Task PostAsJsonAsync_ShouldReturnHttpResponseMessage_WhenRequestIsSuccessful()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var quizHttpClient = new QuizHttpClient(httpClient);
            var data = new { Name = "Test" };

            // Act
            var result = await quizHttpClient.PostAsJsonAsync("http://example.com", data);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Once(),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [TestMethod]
        public async Task PostAsJsonAsync_ShouldThrowTimeoutException_WhenTaskIsCanceled()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .Returns(async () =>
                {
                    // Simulate a delay and then cancel the token
                    await Task.Delay(100);
                    cancellationTokenSource.Cancel();
                    throw new TaskCanceledException("Request was canceled.", null, cancellationToken);
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var quizHttpClient = new QuizHttpClient(httpClient);
            var data = new { Name = "Test" };

            // Act
            Func<Task> act = async () => await quizHttpClient.PostAsJsonAsync("http://example.com", data, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<TimeoutException>()
                .WithMessage("The request timed out.");
        }
    }
}
