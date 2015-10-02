using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBASIC.Interpreter
{
    public delegate object FunctionDelegate(object[] arguments);

    public class InternalFunction : IFunction
    {
        private FunctionDelegate target;

        public InternalFunction(FunctionDelegate target)
        {
            this.target = target;
        }

        public object Invoke(object[] args)
        {
            return this.target(args);
        }
    }
}