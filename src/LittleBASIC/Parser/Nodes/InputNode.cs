using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBASIC.Parser.Nodes
{
    public class InputNode: AstNode
    {
        public AstNode Variable { get { return Children[0]; } }

        public InputNode(AstNode variable)
        {
            Children.Add(variable);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(Lexer.TokenType.Identifier, "INPUT");
            return new InputNode(ExpressionNode.Parse(parser));
        }
    }
}
