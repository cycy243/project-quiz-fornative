using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using Quiz.ForNative.Repository.Exceptions;
using Quiz.ForNative.Tests.Utils.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Middleware.Http.Tests
{
    [TestClass]
    public class ConflictHandlerTests
    {
        [TestMethod]
        public async Task WhenHttpClientGetResponseWithConflictStatusThenThrowError()
        {
            // Arrange
            var handlerMock = HttpMessageHandlerMock.CreateHttpMessageHandler(HttpStatusCode.Conflict, new StringContent("Conflict response content"));

            var unAuthorizedAccessHandler = new ConflictHandler
            {

                InnerHandler = handlerMock.Object
            };

            var httpClient = new HttpClient(unAuthorizedAccessHandler);

            // Act
            Func<Task> action = async () => await httpClient.GetAsync("https://www.google.com");

            // Assert
            await action.Should().ThrowAsync<RessourceAlreadyExistsException>();
        }

        [TestMethod]
        public async Task WhenHttpClientGetResponseWithOkStatusThenDoesNotThrowError()
        {
            // Arrange
            var handlerMock = HttpMessageHandlerMock.CreateHttpMessageHandler(HttpStatusCode.OK, new StringContent("ok response content"));

            var unAuthorizedAccessHandler = new ConflictHandler
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
