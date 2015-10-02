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
        private static Random rnd = new Random();

        public Dictionary<string, InternalFunction> GetFunctions()
        {
            var result = new Dictionary<string, InternalFunction>();
            result.Add("INT", new InternalFunction(INT));
            result.Add("RND", new InternalFunction(RND));

            return result;
        }

        private static object INT(object[] args)
        {
            return Convert.ToDouble(args[0]);
        }

        private static object RND(object[] args)
        {
            if (args.Length <= 0)
                return Convert.ToDouble(rnd.Next());
            else if (args.Length == 1)
                return Convert.ToDouble(rnd.Next(Convert.ToInt32(Convert.ToDouble(args[0]))));
            else
                return Convert.ToDouble(rnd.Next(Convert.ToInt32(Convert.ToDouble(args[0])), Convert.ToInt32(Convert.ToDouble(args[1]))));
        }
    }
}
