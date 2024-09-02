using Quiz.ForNative.Domain;
using Quiz.ForNative.Services.Exceptions;
using Quiz.ForNative.Services.Interface.Auth;
using Quiz.ViewModels.Exceptions;
using Quiz.ViewModels.Interface;

namespace Quiz.ViewModels
{
    public class LoginViewModel : IConnectViewModel
    {
        private IConnectService<User> _connectService;

        public string Login { get; set; }
        public string Password { get; set; }

        public LoginViewModel(IConnectService<User> connectService)
        {
            _connectService = connectService;
        }

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
            catch (Exception ex)
            {
                throw new ViewModelException($"Unexpected error");
            }
        }
    }
}
