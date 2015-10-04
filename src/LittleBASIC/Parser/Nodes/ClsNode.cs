using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBASIC.Parser.Nodes
{
    public class ClsNode: AstNode
    {
        public ClsNode()
        {
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(Lexer.TokenType.Identifier, "CLS");

            return new ClsNode();
        }
    }
}
