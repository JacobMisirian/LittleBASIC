using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBASIC.Lexer
{
    public enum TokenType
    {
        Assignment,
        Line,
        Parentheses,
        Comma,
        Exception,
        Number,
        String,
        Identifier,
        Operation,
        Comparison
    }

    public class Token
    {
        public TokenType TokenType { get; private set; }
        public object Value { get; private set; }

        public Token(TokenType tokenType, object value)
        {
            TokenType = tokenType;
            Value = value;
        }
    }
}
