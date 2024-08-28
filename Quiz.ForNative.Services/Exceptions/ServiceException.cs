using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Services.Exceptions
{
    /// <summary>
    /// Define an exception made to "mask" other exceptions and usable for user display purposes.
    /// </summary>
    public class ServiceException : Exception
    {
        public ServiceException(string message): base(message) { }
    }
}
