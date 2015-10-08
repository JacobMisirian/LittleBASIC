    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleBASIC.Interpreter
{
    interface IFunction
    {
        object Invoke(object[] args);
    }
}
