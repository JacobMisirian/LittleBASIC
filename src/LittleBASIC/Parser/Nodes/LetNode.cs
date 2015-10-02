using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBASIC.Parser.Nodes
{
    public class LetNode: AstNode
    {
        public AstNode Variable { get { return Children[0]; } }
        public AstNode Data { get { return Children[1]; } }

        public LetNode(AstNode variable, AstNode data)
        {
            Children.Add(variable);
            Children.Add(data);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(Lexer.TokenType.Identifier, "LET");
            BinaryOpNode variable = (BinaryOpNode)ExpressionNode.Parse(parser);
            return new LetNode(variable.Left, variable.Right);
        }
    }
}
