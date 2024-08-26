using AutoMapper;
using Quiz.Dtos;
using Quiz.ForNative.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Mappers
{
    public class RegisterMapper: Profile
    {
        public RegisterMapper() 
        {
            CreateMap<RegisterDto, UserDto>()
                .ForMember(dest => dest.Pseudo, opt => opt.MapFrom(src => src.Pseudo))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(dest => dest.PicturePath, opt => opt.MapFrom(src => src.PicturePath))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ReverseMap();
        }
    }
}
