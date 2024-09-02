using Quiz.ForNative.Domain;
using Quiz.ForNative.Services.Exceptions;
using Quiz.ForNative.Services.Interface.Auth;
using Quiz.ViewModels.Exceptions;
using Quiz.ViewModels.Interface;

namespace Quiz.ViewModels
{
    public class LoginViewModel(IConnectService<User> connectService) : IConnectViewModel
    {
        private readonly IConnectService<User> _connectService = connectService;

        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

        public async Task<bool> LogUserIn()
        {
            try
            {
                User? result = await _connectService.ConnectWithCredentialsAsync(Login, Password);
                return result != null;
            }
            catch (ServiceException se)
            {
                throw new ViewModelException(se.Message);
            }
            catch (Exception)
            {
                throw new ViewModelException($"Unexpected error");
            }
        }
    }
}
