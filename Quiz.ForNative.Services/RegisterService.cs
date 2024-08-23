using Quiz.Dtos.Api;
using Quiz.Dtos.App;
using Quiz.ForNative.Args;
using Quiz.ForNative.Repository.Interfaces;
using Quiz.ForNative.Services.Interface;

namespace Quiz.ForNative.Services
{
    public class RegisterService : RegisterService<RegisterDto>
    {
        private IRepository<UserDto, UserSearchArgs> _userRepository;

        public RegisterService(IRepository<UserDto, UserSearchArgs> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegisterDto> RegisterUser(RegisterDto entity)
        {
            bool registerResult = await _userRepository.Add(new UserDto(
                Bio: entity.Bio,
                Email: entity.Email,
                Firstname: entity.Firstname,
                Name: entity.Name,
                Password: entity.Password,
                PicturePath: entity.PicturePath,
                Pseudo: entity.Pseudo
            ));
            return registerResult ? entity : throw new Exception("An error occured while saving the user");
        }
    }
}
