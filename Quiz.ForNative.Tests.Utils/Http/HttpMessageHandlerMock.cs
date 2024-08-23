using Moq;
using Moq.Protected;
using System.Net;

namespace Quiz.ForNative.Tests.Utils.Http
{
    public static class HttpMessageHandlerMock
    {
        public static Mock<HttpMessageHandler> CreateHttpMessageHandler(HttpStatusCode statusCode, HttpContent messageContent)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = messageContent
                });
            return handlerMock;
        }
    }
}
