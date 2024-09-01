using AutoMapper;
using Quiz.Dtos;
using Quiz.ForNative.Domain;
using Quiz.ForNative.Repository.Exceptions;
using Quiz.ForNative.Repository.Interfaces;
using Quiz.ForNative.Services.Exceptions;
using Quiz.ForNative.Services.Interface.Auth;

namespace Quiz.ForNative.Services
{
    public class ConnectService : IConnectService<User>
    {
        private IAuthRepository<UserDto> _authRepository;
        private IMapper _mapper;

        public ConnectService(IAuthRepository<UserDto> authRepository, IMapper mapper) 
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<User?> ConnectWithCredentialsAsync(string login, string password)
        {
            try
            {
                UserDto? userDto = await _authRepository.GetUserByCredentialsAsync(new CredentialsArgs(login, password));
                return userDto == null ? null : _mapper.Map<User>(userDto);
            }
            catch(TimeoutException)
            {
                throw new ServiceException("The server is unreachable");
            }
            catch (ResourceNotFoundException)
            {
                throw new ServiceException("A user with the given email or pseudo already exists");
            }
            catch (ValidationException ve)
            {
                string error = string.Join("\n-", ve.Errors);
                throw new ServiceException($"One or multiple field aren't valid: \n-{error}");
            }
        }
    }
}
