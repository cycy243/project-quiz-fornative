using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Components.Form
{
    public delegate string InputValidationFunction(string inputName, object value);

    interface IFormInput<T>
    {
        string LabelContent { get; set; }
        T PlaceholderContent { get; set; }
        InputValidationFunction ValidationFunction { get; set; }
        string InputName { get; set; }
        bool Validate();
    }
}
