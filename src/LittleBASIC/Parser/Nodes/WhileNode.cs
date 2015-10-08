using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LittleBASIC.Parser.Nodes
{
    public class WhileNode: AstNode
    {
        public AstNode Predicate { get { return Children[0]; } }
        public AstNode Body { get { return Children[1]; } }

        public WhileNode(AstNode predicate, AstNode body)
        {
            Children.Add(predicate);
            Children.Add(body);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(Lexer.TokenType.Identifier, "WHILE");
            return new WhileNode(ExpressionNode.Parse(parser), StatementNode.Parse(parser));
        }
    }
}
