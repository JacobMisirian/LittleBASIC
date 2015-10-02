using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBASIC.Interpreter;

namespace LittleBASIC.Functions
{
    public class StandardLibrary: ILibrary
    {
        public Dictionary<string, InternalFunction> GetFunctions()
        {
            var result = new Dictionary<string, InternalFunction>();
            result.Add("INT", new InternalFunction(INT));

            return result;
        }

        private static object INT(object[] args)
        {
            return Convert.ToDouble(args[0]);
        }
    }
}
