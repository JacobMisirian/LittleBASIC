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
 
            result.Add("ABS", new InternalFunction(ABS));
            result.Add("ATN", new InternalFunction(ATN));
            result.Add("COS", new InternalFunction(COS));
            result.Add("EXP", new InternalFunction(EXP));
            result.Add("INT", new InternalFunction(INT));
            result.Add("LOG", new InternalFunction(LOG));
            result.Add("RND", new InternalFunction(RND));
            result.Add("SIN", new InternalFunction(SIN));
            result.Add("SQR", new InternalFunction(SQR));
            result.Add("TAN", new InternalFunction(TAN));

            return result;
        }

        private static object ABS(object[] args)
        {
            return Convert.ToDouble(Math.Abs(Convert.ToDouble(args[0])));
        }

        private static object ATN(object[] args)
        {
            return Convert.ToDouble(Math.Atan(Convert.ToDouble(args[0])));
        }

        private static object COS(object[] args)
        {
            return Convert.ToDouble(Math.Cos(Convert.ToDouble(args[0])));
        }

        private static object EXP(object[] args)
        {
            return Convert.ToDouble(Math.Exp(Convert.ToDouble(args[0])));
        }

        private static object INT(object[] args)
        {
            return Convert.ToDouble(args[0]);
        }

        private static object LOG(object[] args)
        {
            return Convert.ToDouble(Math.Log(Convert.ToDouble(args[0])));
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

        private static object SIN(object[] args)
        {
            return Convert.ToDouble(Math.Sin(Convert.ToDouble(args[0])));
        }

        private static object SQR(object[] args)
        {
            return Convert.ToDouble(Math.Sqrt(Convert.ToDouble(args[0])));
        }

        private static object TAN(object[] args)
        {
            return Convert.ToDouble(Math.Tan(Convert.ToDouble(args[0])));
        }
    }
}
