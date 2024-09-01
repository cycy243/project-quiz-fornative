using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quiz.Dtos;
using Quiz.ForNative.Domain;
using Quiz.ForNative.Mappers;
using Quiz.ForNative.Repository.Exceptions;
using Quiz.ForNative.Repository.Interfaces;
using Quiz.ForNative.Services.Exceptions;
using Quiz.ForNative.Services.Interface.Auth;

namespace Quiz.ForNative.Services.Tests
{
    [TestClass]
    public class ConnectServiceTests
    {
        public required IConnectService<User> _connectService;
        public required Mock<IAuthRepository<UserDto>> _mockedAuthRepo;

        [TestInitialize]
        public void SetUp()
        {
            _mockedAuthRepo = new Mock<IAuthRepository<UserDto>>(); 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapperProfile());
            });
            IMapper mapper = new Mapper(config);

            _connectService = new ConnectService(_mockedAuthRepo.Object, mapper);
        }

        [TestMethod]
        [DynamicData(nameof(WhenRepositoryThrowErrorThenServiceMaskItWithItsOwnData), DynamicDataSourceType.Property)]
        public async Task WhenRepositoryThrowErrorThenServiceMaskItWithItsOwn(Exception exception)
        {
            // Arrange
            _mockedAuthRepo.Setup(mar => mar.GetUserByCredentialsAsync(It.IsAny<CredentialsArgs>()))
                .ThrowsAsync(exception);
            Type exceptionType = exception.GetType();

            // Act
            Func<Task> action = async () => await _connectService.ConnectWithCredentialsAsync("", "");

            // Assert
            await action.Should()
                .ThrowAsync<ServiceException>();
        }

        public static IEnumerable<object[]> WhenRepositoryThrowErrorThenServiceMaskItWithItsOwnData
        {
            get
            {

                return
                [
                    [new ResourceNotFoundException("")],
                    [new ValidationException("", [])],
                    [new TimeoutException("")]
                ];
            }
        }
    }
}