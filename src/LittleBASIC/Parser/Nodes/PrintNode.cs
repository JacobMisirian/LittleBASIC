using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleBASIC.Parser.Nodes
{
    public class PrintNode: AstNode
    {
        public AstNode Value { get; private set; }

        public PrintNode (AstNode value)
        {
            Value = value;
        }
        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(Lexer.TokenType.Identifier);
            return new PrintNode(ExpressionNode.Parse(parser));
        }
    }
}
