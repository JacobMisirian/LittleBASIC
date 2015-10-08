using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LittleBASIC.Lexer;

namespace LittleBASIC.Parser.Nodes
{
    public class ArgListNode: AstNode
    {
        public static ArgListNode Parse(Parser parser)
        {
            ArgListNode ret = new ArgListNode();
            parser.ExpectToken(TokenType.Parentheses, "(");

            while (!parser.MatchToken(TokenType.Parentheses, ")"))
            {
                ret.Children.Add(ExpressionNode.Parse(parser));
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            parser.ExpectToken(TokenType.Parentheses, ")");

            return ret;
        }
    }
}
