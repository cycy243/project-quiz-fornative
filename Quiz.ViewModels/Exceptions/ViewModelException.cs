﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ViewModels.Exceptions
{
    public class ViewModelException: Exception
    {
        public ViewModelException(string message): base(message) { }
    }
}
