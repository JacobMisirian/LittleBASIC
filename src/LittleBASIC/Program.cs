using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBASIC.Lexer;

namespace LittleBASIC
{
    class Program
    {
        static void Main(string[] args)
        {

            new Interpreter.Interpreter(new Parser.Parser(new Lexer.Lexer(File.ReadAllText("test.txt")).Tokenize()).Parse()).Interpret();

            Console.ReadKey();
        }
    }
}
