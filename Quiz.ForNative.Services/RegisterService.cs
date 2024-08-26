﻿using Quiz.ForNative.Args;
using Quiz.ForNative.Repository.Interfaces;
using Quiz.ForNative.Services.Interface;
using Quiz.Dtos;
using AutoMapper;
using System.Net.Http.Headers;

namespace Quiz.ForNative.Services
{
    public class RegisterService : RegisterService<RegisterDto>
    {
        private IAuthRepository<UserDto> _authRepository;
        private IMapper _mapper;

        public RegisterService(IAuthRepository<UserDto> authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<RegisterDto> RegisterUser(RegisterDto entity)
        {
            try
            {
                UserDto registerResult = await _authRepository.RegisterUser(_mapper.Map<UserDto>(entity), File.OpenRead(entity.PicturePath));
                return entity ?? throw new Exception("An error occured while saving the user");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
