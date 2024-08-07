using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Components.Form
{
    interface IFormInput<T>
    {
        string LabelContent { get; set; }
        T PlaceholderContent { get; set; }
    }
}
