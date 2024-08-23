using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository.Exceptions
{
    public class RessourceAlreadyExistsException: Exception
    {
        public RessourceAlreadyExistsException(string message): base(message) { }
    }
}
