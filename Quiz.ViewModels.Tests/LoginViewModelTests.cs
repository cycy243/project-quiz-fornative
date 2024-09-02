using Moq;
using Quiz.ForNative.Domain;
using Quiz.ForNative.Repository.Exceptions;
using Quiz.ForNative.Services.Exceptions;
using Quiz.ForNative.Services;
using Quiz.ForNative.Services.Interface.Auth;
using Quiz.ViewModels.Interface;
using FluentAssertions;
using Quiz.ViewModels.Exceptions;

namespace Quiz.ViewModels.Tests
{
    [TestClass()]
    public class LoginViewModelTests
    {
        public required Mock<IConnectService<User>> _mockedConnectService;
        public required IConnectViewModel _viewModel;

        [TestInitialize]
        public void SetUp()
        {
            _mockedConnectService = new Mock<IConnectService<User>>();
            _viewModel = new LoginViewModel(_mockedConnectService.Object);
        }

        [TestMethod]
        [DynamicData(nameof(WhenServiceThrowExceptionsThenViewModelCatchItAndThrowsItsOwnData), DynamicDataSourceType.Property)]
        public async Task WhenServiceThrowExceptionsThenViewModelCatchItAndThrowsItsOwn(Exception exception)
        {
            // Arrange
            _mockedConnectService.Setup(mcs => mcs.ConnectWithCredentialsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(exception);

            // Act
            _viewModel.Login = "Coucou";
            _viewModel.Password = "Coucou";
            Func<Task> actionConnectWithCredentialsAsync = async () => await _viewModel.LogUserIn();

            // Assert
            if(exception.GetType() == typeof(ServiceException))
            {
                await actionConnectWithCredentialsAsync.Should()
                    .ThrowAsync<ViewModelException>()
                    .Where(e => e.Message.Equals(exception.Message));
            }
            else
            {
                await actionConnectWithCredentialsAsync.Should()
                    .ThrowAsync<ViewModelException>()
                    .Where(e => e.Message.ToLower().Contains("unexpected error"));
            }
        }

        [TestMethod]
        public async Task WhenUserIsFoundWhenLogUserInThenReturnTrue()
        {
            // Arrange
            bool? result = null;
            _mockedConnectService.Setup(mcs => mcs.ConnectWithCredentialsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new User("", "", "", "", "", "", ""));

            // Act
            _viewModel.Login = "Coucou";
            _viewModel.Password = "Coucou";
            Func<Task> action = async () => result = await _viewModel.LogUserIn();

            // Assert
            await action.Should()
                .NotThrowAsync<ViewModelException>();
            result.Should()
                .NotBeNull()
                .And
                .BeTrue();
        }

        public static IEnumerable<object[]> WhenServiceThrowExceptionsThenViewModelCatchItAndThrowsItsOwnData
        {
            get
            {

                return
                [
                    [new ResourceNotFoundException("")],
                    [new ValidationException("", [])],
                    [new TimeoutException("")],
                    [new NotImplementedException("")],
                    [new ArgumentNullException("")],
                    [new TaskCanceledException("")],
                    [new ServiceException("")]
                ];
            }
        }
    }
}