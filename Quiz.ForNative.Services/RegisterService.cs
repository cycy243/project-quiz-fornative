using ApiDtos = Quiz.Dtos.Api;
using AppDtos = Quiz.Dtos.App;
using Quiz.ForNative.Args;
using Quiz.ForNative.Repository.Interfaces;
using Quiz.ForNative.Services.Interface;

namespace Quiz.ForNative.Services
{
    public class RegisterService : RegisterService<ApiDtos.RegisterDto>
    {
        private IRepository<ApiDtos.UserDto, UserSearchArgs> _userRepository;

        public RegisterService(IRepository<ApiDtos.UserDto, UserSearchArgs> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiDtos.RegisterDto> RegisterUser(ApiDtos.RegisterDto entity)
        {
            bool registerResult = await _userRepository.Add(new ApiDtos.UserDto(
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
