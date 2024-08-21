using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository
{
    public class UnAuthorizedAccessException: Exception
    {
        public UnAuthorizedAccessException(string message): base(message) { }
    }
}
