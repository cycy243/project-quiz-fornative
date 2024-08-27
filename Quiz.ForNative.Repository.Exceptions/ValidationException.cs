using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository.Exceptions
{
    public class ValidationException : Exception
    {
        public string[] Errors { get; private set; }

        public ValidationException(string message, string[] errors) : base(message) 
        {
            Errors = errors;
        }
    }
}
