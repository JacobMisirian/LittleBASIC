using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBASIC.Parser.Nodes
{
    public class GotoNode: AstNode
    {
        public double Position { get; private set; }

        public GotoNode(double position)
        {
            Position = position;
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(Lexer.TokenType.Identifier, "GOTO");

            return new GotoNode(((NumberNode)ExpressionNode.Parse(parser)).Value);
        }
    }
}
