using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleBASIC.Parser.Nodes
{
    public class GotoNode: AstNode
    {
        public string Position { get; private set; }

        public GotoNode(string position)
        {
            Position = position;
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(Lexer.TokenType.Identifier, "GOTO");

            AstNode position = ExpressionNode.Parse(parser);

            if (position is NumberNode)
                return new GotoNode(Convert.ToString(((NumberNode)position).Value));
            else if (position is IdentifierNode)
                return new GotoNode(((IdentifierNode)position).Identifier);
            else
                throw new Exception("Unknown type " + parser.CurrentToken().TokenType + " " + parser.CurrentToken().Value);
        }
    }
}
