using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using LittleBASIC.Lexer;

namespace LittleBASIC
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length <= 0) //
                Interactive.Enter();
            else
                new Interpreter.Interpreter(new Parser.Parser(new Lexer.Lexer(File.ReadAllText(args[0])).Tokenize()).Parse()).Interpret();

            Console.ReadKey();
        }
    }
}
