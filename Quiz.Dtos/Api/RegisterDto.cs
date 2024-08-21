using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Dtos.Api
{
    public record RegisterDto(string Name, string Firstname, string Pseudo, string Password, string Email, string PicturePath, string Bio);
}
