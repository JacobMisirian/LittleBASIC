using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBASIC.Lexer
{
    public class Lexer
    {
        private string code { get; set; }
        private int position { get; set; }
        private List<Token> result = new List<Token>();

        public Lexer(string code)
        {
            this.code = code;
            position = 0;
        }

        public List<Token> Tokenize()
        {
            whiteSpace();

            while (peekChar() != -1)
            {
                if (char.IsLetterOrDigit((char)peekChar()))
                    result.Add(scanData());
                else if ((char)peekChar() == '\"')
                    result.Add(scanString());
                else if ((char)peekChar() == '\'')
                    scanComment();
                else if ((char)peekChar() == '(' || ((char)peekChar() == ')'))
                    result.Add(new Token(TokenType.Parentheses, ((char)readChar()).ToString()));
                else if ((char)peekChar() == ',')
                    result.Add(new Token(TokenType.Comma, ((char)readChar()).ToString()));
                else if ((char)peekChar() == ':')
                    result.Add(new Token(TokenType.Identifier, ((char)readChar()).ToString()));
                else if ((char)peekChar() == '=' && (char)peekChar(1) == '=')
                    result.Add(new Token(TokenType.Comparison, (char)readChar() + "" + (char)readChar()));
                else if ("+-/*%".Contains((((char)peekChar()).ToString())))
                    result.Add(new Token(TokenType.Operation, ((char)readChar()).ToString()));
                else if ("<>!".Contains((((char)peekChar()).ToString())))
                    result.Add(new Token(TokenType.Comparison, ((char)readChar()).ToString()));
                else if ((char)peekChar() == '=')
                    result.Add(new Token(TokenType.Assignment, ((char)readChar()).ToString()));
                else
                {
                    result.Add(new Token(TokenType.Exception, "Unexpected " + ((char)peekChar()).ToString() + " encountered"));
                    readChar();
                }

                whiteSpace();
            }

            return result;
        }

        private Token scanData()
        {
            string result = "";
            double temp = 0;
            while ((char.IsLetterOrDigit((char)peekChar()) && peekChar() != -1) || ((char)(peekChar()) == '.'))
                result += ((char)readChar()).ToString();
            if (double.TryParse(result, out temp))
                return new Token(TokenType.Number, result);

            return new Token(TokenType.Identifier, result);
        }

        private Token scanString()
        {
            string result = "";
            readChar();

            while (peekChar() != -1 && peekChar() != '\"')
                result += ((char)readChar()).ToString();

            readChar();

            return new Token(TokenType.String, result);
        }

        private void scanComment()
        {
            readChar();

            while (peekChar() != '\n')
                readChar();
        }

        private void whiteSpace()
        {
            while (char.IsWhiteSpace((char)peekChar()))
                readChar();
        }

        private int peekChar(int n = 0)
        {
            if (position + n < code.Length)
                return code[position + n];

            return -1;
        }

        private int readChar()
        {
            if (position < code.Length)
                return code[position++];

            return -1;
        }
    }
}
