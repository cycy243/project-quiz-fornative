using Quiz.Dtos;
using Quiz.ForNative.Services.Interface;
using Quiz.ViewModels.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ViewModels
{
    public class RegisterViewModel : IRegisterViewModel
    {
        private RegisterService<RegisterDto> _registerService;
        public RegisterViewModel(RegisterService<RegisterDto> registerService) 
        {
            _registerService = registerService;
        }

        public async Task<bool> RegisterUser(RegisterUserArgs args)
        {
            return (await _registerService.RegisterUser(new RegisterDto(args.Name, args.Firstname, args.Pseudo, args.Password, args.Email, args.PicturePath, args.Bio, args.BirthDate))) != null;
        }
    }
}
